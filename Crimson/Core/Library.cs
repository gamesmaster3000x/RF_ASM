using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.Exceptions;
using Crimson.CURI;
using Crimson.Parsing;
using NLog;

namespace Crimson.Core
{
    /// <summary>
    /// Generates CompilationUnits from input text with the power of ANTLR.
    /// </summary>
    public class Library
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private static int _loaderIdCounter = 0;
        private static object _loaderLock = new object();
        public static int LoaderId
        {
            get
            {
                lock (_loaderLock)
                    return _loaderIdCounter;
            }

            set
            {
                lock (_loaderLock)
                    _loaderIdCounter = value;
            }
        }

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

        public async Task<List<Scope>> GetScopes ()
        {
            List<Scope> scopes = new List<Scope>();

            foreach (var pair in Scopes)
            {
                Task<Scope> task = pair.Value;

                // TODO Key non-null, task null here
                if (task.Status == TaskStatus.Created)
                    task.Start();

                try
                {
                    scopes.Add(await task);
                }
                catch (Exception ex)
                {
                    Crimson.Panic("Unable to GetScopes", Crimson.PanicCode.COMPILE_PARSE, ex);
                    throw;
                }
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

        public Scope LoadScope (AbstractCURI uri)
        {
            try
            {
                LOGGER.Info($"Loading scope from {uri}");

                CachedBerryClient.GetCachedQueryResult result = CachedBerryClient.GetOrInstall(uri);
                if (!result.Exists) throw new NullReferenceException($"Library was unable to get cached or fetch new contents of {uri}.");

                Scope scope = ParseScopeText(uri, result.Contents!);

                LoadScopeDependencies(scope);

                // Only runs once (for root)
                if (Root == null)
                {
                    SetRootScope(scope);

                    // Wait for dependencies to load
                    bool waiting;
                    do
                    {
                        List<string> waitingList = new List<string>();
                        foreach (var pair in Scopes)
                            if (pair.Value == null) waitingList.Add(pair.Key.ToString());

                        waiting = waitingList.Count > 0;

                        if (waiting)
                        {
                            LOGGER.Info($"Waiting for: {string.Join(',', waitingList)}");
                            Thread.Sleep(1000);
                        }
                        else
                            LOGGER.Info($"Finished loading root scope!");

                    } while (waiting);
                }

                //
                return scope;
            }
            catch (Exception ex)
            {
                Crimson.Panic($"Error loading scope from {uri}", Crimson.PanicCode.COMPILE_PARSE_SCOPE, ex);
                throw;
            }
        }

        private Task<Scope> GetScopeLoadingTask (AbstractCURI uri)
        {
            // Check if already loading/loaded and reserve key if not
            if (Scopes.ContainsKey(uri)) return Scopes[uri];
            Scopes[uri] = null!;

            // Kept seperate for debugging purposes
            Func<Task<Scope>?> func = async () =>
            {
                try
                {
                    int id = LoaderId++;
                    LOGGER.Info($"Delegating loading of {uri} to worker [{id}]");
                    Thread.CurrentThread.Name = $"CrimsonScopeLoaderWorker[{id}]";
                    return LoadScope(uri);
                }
                catch (Exception ex)
                {
                    Crimson.Panic($"An error occurred while async loading {uri}", Crimson.PanicCode.COMPILE_PARSE_SCOPE_ASYNC, ex);
                    throw;
                }
            };

            // Generate task
            Task<Scope> task = Task.Run(func);

            // Assign correct non-null task to key
            Scopes[uri] = task;
            return task;
        }

        public override string ToString ()
        {
            return $"Library(Units:[{string.Join(',', Scopes.Select(u => u.Key))}])";
        }


        // ========== INTERNALS ==========


        private Scope GetScopeUnsafe (AbstractCURI uri)
        {
            if (!Scopes.TryGetValue(uri, out Task<Scope>? task))
            {
                LOGGER.Error("UNSAFE NULL");
                throw new ArgumentNullException("UNSAFE NULL");
            }
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
        private void LoadScopeDependencies (Scope root)
        {
            try
            {
                List<Task> ongoingLoadingTasks = new List<Task>();

                // Load each imported scope
                // Queue loading of its dependencies (once it's loaded)
                LOGGER.Debug($"Dependencies for {root} are {string.Join(", ", root.Imports.Values.Select((i) => $"'{i.CURI}'"))}");
                foreach (var i in root.Imports)
                {
                    if (Scopes.ContainsKey(i.Value.CURI))
                    {
                        LOGGER.Debug($"Skipping duplicate loading of {i.Value.CURI}");
                        continue;
                    }

                    Task<Scope> scope = GetScopeLoadingTask(i.Value.CURI);
                    Task dependencyTask = scope.ContinueWith(finishedTask => LoadScopeDependencies(finishedTask.Result));

                    ongoingLoadingTasks.Add(dependencyTask);
                }

                // Check for imports in nested scopes
                foreach (var del in root.Delegates)
                    if (del.Invoke() is IHasScope hasScope)
                        LoadScopeDependencies(hasScope.GetScope());
            }
            catch (Exception ex)
            {
                Crimson.Panic($"Error loading scope dependencies for {root}", Crimson.PanicCode.COMPILE_PARSE_SCOPE_DEPS, ex);
                throw;
            }
        }

        private Scope ParseScopeText (AbstractCURI source, char[] data)
        {
            try
            {
                LOGGER.Debug($"Parsing {data.Length} characters from {source} with ANTLR...");
                string sourceName = $"{source}";

                // Get Antlr context
                AntlrInputStream a4is = new AntlrInputStream(data, data.Length);

                CrimsonLexer lexer = new CrimsonLexer(a4is);
                lexer.AddErrorListener(new LexerErrorListener(sourceName));

                CommonTokenStream cts = new CommonTokenStream(lexer);
                CrimsonParser parser = new CrimsonParser(cts);
                parser.AddErrorListener(new ParserErrorListener(sourceName));

                CrimsonParser.ScopeContext cuCtx = parser.scope();
                ScopeVisitor visitor = new ScopeVisitor();

                Scope scope = visitor.VisitScope(cuCtx);
                scope.CURI = source;
                return scope;
            }
            catch (Exception ex)
            {
                Crimson.Panic($"An error ocurred while parsing a scope originating from {source}", Crimson.PanicCode.COMPILE_PARSE_SCOPE, ex);
                throw;
            }
        }
    }
}
