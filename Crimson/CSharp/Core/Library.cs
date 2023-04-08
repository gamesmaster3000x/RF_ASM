using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Core.CURI;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing;
using NLog;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Generates CompilationUnits from input text with the power of ANTLR.
    /// </summary>
    public class Library
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The "master" list of units. Stored so that each unique unit is only created once.
        /// 
        /// For example:
        /// [ C:/utils.crm => UTILS_UNIT ]
        /// [ C:/main.crm  => MAIN_UNIT  ]
        /// </summary>
        private ConcurrentDictionary<AbstractCURI, Task<Scope>> Scopes { get; }

        public Scope Root { get; set; }

        public Library ()
        {
            Scopes = new ConcurrentDictionary<AbstractCURI, Task<Scope>>();
        }


        // ========== API ==========

        public Scope? GetScope (AbstractCURI curi)
        {
            return GetScopeUnsafe(curi);
        }

        public List<Scope> GetScopes ()
        {
            List<Scope> scopes = new List<Scope>();

            foreach (var pair in Scopes)
            {
                Task<Scope> task = pair.Value;

                // TODO Key non-null, task null here
                if (task.Status == TaskStatus.Created)
                    task.Start();

                if (!task.IsCompleted)
                {
                    // TODO freezing here
                    LOGGER.Debug("Waiting for async loading to finish before returning scope list...");
                    task.Wait();
                }

                scopes.Add(pair.Value.Result!);
            }

            return scopes;
        }

        public void SetRootScope (Scope scope)
        {
            if (Root != null)
                LOGGER.Warn($"Overriding root scope {Root} with {scope}");
            else
                LOGGER.Info($"Setting root scope to {scope}");

            Root = scope;
        }

        public async Task<Scope> LoadScope (AbstractCURI uri)
        {
            LOGGER.Info($"Loading scope from {uri}");

            Stream source = await GetStreamOf(uri);
            StreamReader reader = new StreamReader(source);
            string text = reader.ReadToEnd();

            Scope scope = ParseScopeText(uri, text);

            LoadScopeDependencies(scope);

            // Only runs once (for root)
            if (Root == null)
            {
                SetRootScope(scope);

                bool waiting;
                do
                {
                    List<string> waitingList = new List<string>();
                    foreach (var pair in Scopes)
                    {
                        if (pair.Value == null) waitingList.Add(pair.Key.ToString());
                    }

                    waiting = waitingList.Count > 0;

                    if (waiting)
                    {
                        LOGGER.Info($"Waiting for: {String.Join(',', waitingList)}");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        LOGGER.Info($"Finished loading root scope!");
                    }

                } while (waiting);
            }

            return scope;
        }

        private async Task<Task<Scope>> LoadScopeAsync (AbstractCURI uri)
        {
            // Check if already loading/loaded and reserve key if not
            if (Scopes.ContainsKey(uri)) return Scopes[uri];
            Scopes[uri] = null!;

            // Generate task
            Task<Scope> task = await Task.Factory.StartNew(async () =>
            {
                Thread.CurrentThread.Name = $"{Thread.CurrentThread.Name}_{uri.ToString()}";
                return await LoadScope(uri);
            });

            // Assign correct non-null task to key
            Scopes[uri] = task;
            return task;
        }

        public override string ToString ()
        {
            return $"Library(Units:[{String.Join(',', Scopes.Select(u => u.Key))}])";
        }


        // ========== INTERNALS ==========


        private Scope GetScopeUnsafe (AbstractCURI uri)
        {
            if (!Scopes.TryGetValue(uri, out Task<Scope>? task))
                throw new ArgumentNullException("UNSAFE NULL");
            if (!task.IsCompleted)
            {
                LOGGER.Debug($"Waiting for scope {uri} to finish loading...");
                task.Wait();
            }

            return task.Result;
        }

        /// <summary>
        /// Loads dependencies for the given root CompilationUnit, as well as that unit's dependencies, recursively.
        /// Also checks for nested scopes and loads them as well!
        /// </summary>
        /// <param name="root"></param>
        private async void LoadScopeDependencies (Scope root)
        {
            List<Task> ongoingLoadingTasks = new List<Task>();

            // Load each imported scope
            // Queue loading of its dependencies (once it's loaded)
            foreach (var i in root.Imports)
            {
                if (Scopes.ContainsKey(i.Value.CURI))
                {
                    LOGGER.Debug($"Skipping duplicate loading of {i.Value.CURI}");
                    continue;
                }
                LOGGER.Debug($"Trying to load {i.Value.CURI}");

                Task<Scope> scope = await LoadScopeAsync(i.Value.CURI);
                Task dependencyTask = scope.ContinueWith(finishedTask => LoadScopeDependencies(finishedTask.Result));

                ongoingLoadingTasks.Add(dependencyTask);
            }

            // Check for imports in nested scopes
            foreach (var del in root.Delegates)
            {
                if (del.Invoke() is IHasScope hasScope)
                {
                    LoadScopeDependencies(hasScope.GetScope());
                }
            }
        }

        private Scope ParseScopeText (AbstractCURI source, string textIn)
        {
            try
            {
                LOGGER.Debug($"Parsing {textIn.Length} characters from {source} with ANTLR...");

                // Get Antlr context
                AntlrInputStream a4is = new AntlrInputStream(textIn);
                CrimsonLexer lexer = new CrimsonLexer(a4is);
                CommonTokenStream cts = new CommonTokenStream(lexer);
                CrimsonParser parser = new CrimsonParser(cts);

                string sourceName = $"{source}";

                lexer.AddErrorListener(new LexerErrorListener(sourceName));
                parser.ErrorHandler = new ParserErrorStrategy(sourceName);

                CrimsonParser.ScopeContext cuCtx = parser.scope();
                ScopeVisitor visitor = new ScopeVisitor();
                Scope scope = visitor.VisitScope(cuCtx);

                scope.CURI = source;

                return scope;
            }
            catch (Exception ex)
            {
                Crimson.Panic($"An error ocurred while parsing a scope originating from {source}", Crimson.PanicCode.PARSE_SCOPE, ex);
                throw;
            }
        }

        private async Task<Stream> GetStreamOf (AbstractCURI uri)
        {
            try
            {
                return await uri.GetStream();
            }
            catch (Exception e)
            {
                Crimson.Panic($"An error occurred obtaining a stream of the resource at the URI {uri}", Crimson.PanicCode.PARSE, e);
                throw;
            }
        }
    }
}
