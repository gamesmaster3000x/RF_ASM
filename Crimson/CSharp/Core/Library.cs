using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing;
using NLog;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Generates CompilationUnits from input text with the power of ANTLR.
    /// </summary>
    internal class Library
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static readonly string NATIVE_HOST = "crimson.native";
        public static readonly string ROOT_HOST = "crimson.root";

        /// <summary>
        /// The "master" list of units. Stored so that each unique unit is only created once.
        /// 
        /// For example:
        /// [ C:/utils.crm => UTILS_UNIT ]
        /// [ C:/main.crm  => MAIN_UNIT  ]
        /// </summary>
        private ConcurrentDictionary<Uri, Task<Scope>> Scopes { get; }

        public Scope Root { get; set; }

        public Library ()
        {
            Scopes = new ConcurrentDictionary<Uri, Task<Scope>>();
        }


        // ========== API ==========

        public Scope? GetScope (Uri uri)
        {
            if (Scopes.ContainsKey(uri))
            {
                return GetScopeUnsafe(uri);
            }
            else
            {
                Uri nativePath = SquashUri(uri);
                if (Scopes.ContainsKey(nativePath))
                {
                    return GetScopeUnsafe(nativePath);
                }
            }
            return null;
        }

        public List<Scope> GetScopes ()
        {
            List<Scope> scopes = new List<Scope>();

            foreach (var pair in Scopes)
            {
                Task<Scope> task = pair.Value;

                if (!task.IsCompleted)
                {
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

        public Task<Scope> StartLoadingScope (Uri uri)
        {
            // Safety check to prevent double loading
            // Immediately insert the key to reserve it
            Scope? existingScope = GetScope(uri);
            if (existingScope != null) return Task.FromResult(existingScope);
            Scopes[uri] = null!;

            LOGGER.Info($"Loading scope from {uri}");

            // Generate task
            //return Task.Factory.StartNew(() => LoadScopeSync(uri));
            Task<Scope> task = new Task<Scope>(() =>
            {
                Thread.CurrentThread.Name = $"{Thread.CurrentThread.Name}_c";
                return LoadScopeSync(uri);
            });
            task.Start();
            return task;
        }

        public override string ToString ()
        {
            return $"Library(Units:[{String.Join(',', Scopes.Select(u => u.Key))}])";
        }


        // ========== INTERNALS ==========


        private Scope GetScopeUnsafe (Uri uri)
        {
            Task<Scope> task = Scopes[uri];
            if (!task.IsCompleted)
            {
                LOGGER.Debug($"Waiting for scope {uri} to finish loading...");
                task.Wait();
            }
            return task.Result;
        }

        private Scope LoadScopeSync (Uri uri)
        {
            Stream source = GetStreamOf(uri);
            StreamReader reader = new StreamReader(source);
            string text = reader.ReadToEnd();

            Scope scope = ParseScopeText(uri, text);

            LoadScopeDependencies(scope);

            if (Root == null)
            {
                SetRootScope(scope);
            }

            return scope;
        }

        /// <summary>
        /// Loads dependencies for the given root CompilationUnit, as well as that unit's dependencies, recursively.
        /// Also checks for nested scopes and loads them as well!
        /// </summary>
        /// <param name="root"></param>
        private async void LoadScopeDependencies (Scope root)
        {
            LOGGER.Info($"Loading dependencies of {root}");
            List<Task> ongoingLoadingTasks = new List<Task>();

            // Load each imported scope
            // Queue loading of its dependencies (once it's loaded)
            foreach (var i in root.Imports)
            {
                Task<Scope> scope = StartLoadingScope(i.Value.URI);
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

            // Wait for multithreading to finish before returning.
            foreach (var task in ongoingLoadingTasks) await task;
            LOGGER.Debug($"Finished loading dependencies of {root}");
        }

        private Scope ParseScopeText (Uri source, string textIn)
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

                scope.Uri = source;

                return scope;
            }
            catch (Exception ex)
            {
                LOGGER.Error("An error ocurred while parsing a scope");
                Console.WriteLine("An error ocurred while parsing a scope");
                throw new StatementParseException("A parser error occurred :( ", ex);
            }
        }

        private Stream GetStreamOf (Uri uri)
        {
            uri = SquashUri(uri);
            FileStream stream = new FileStream(uri.LocalPath, FileMode.Open);
            return stream;
        }

        private Uri SquashUri (Uri uri)
        {
            UriBuilder builder = new UriBuilder(uri);

            if (uri.Scheme != Uri.UriSchemeFile)
                throw new UriFormatException($"Crimson only accepts URIs of the file:/// scheme at this time. Found: {uri.Scheme}");

            // file:///crimson.native/heap.crm
            if (uri.Host.Equals(NATIVE_HOST))
            {
                string localPath = Path.Combine(Crimson.Options.NativeUri.AbsolutePath, uri.AbsolutePath);
                builder.Host = "";
                builder.Path = localPath;
            }

            // file:///heap.crm
            if (!Path.IsPathRooted(uri.AbsolutePath))
            {
                string? parentDirectory = Path.GetDirectoryName(Crimson.Options.SourceUri.AbsolutePath);
                string localPath = Path.Combine(parentDirectory, uri.AbsolutePath);
                builder.Path = localPath;
            }

            return builder.Uri;
        }
    }
}
