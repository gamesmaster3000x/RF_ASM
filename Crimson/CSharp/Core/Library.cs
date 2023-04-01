using System;
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
        public ConcurrentDictionary<Uri, Scope> Units { get; }

        public Scope Root { get; set; }

        public Library ()
        {
            Units = new ConcurrentDictionary<Uri, Scope>();
        }

        public Scope GetScope (Uri uri)
        {
            if (Units.ContainsKey(uri))
            {
                return Units[uri];
            }
            else
            {
                Uri nativePath = SquashUri(uri);
                if (Units.ContainsKey(nativePath))
                {
                    return Units[nativePath];
                }
            }
            return null;
        }

        public void SetRootScope (Scope scope)
        {
            if (Root != null)
                LOGGER.Warn($"Overriding root scope {Root} with {scope}");
            else
                LOGGER.Info($"Setting root scope to {scope}");

            Root = scope;
        }

        public Task<Scope> LoadScopeAsync (Uri uri)
        {
            // Safety check to prevent double loading
            // Immediately insert the key to reserve it
            Scope existingScope = GetScope(uri);
            if (existingScope != null) return Task.FromResult(existingScope);
            Units[uri] = null;

            LOGGER.Info($"Loading scope from {uri}");

            // Generate task
            return Task.Factory.StartNew(() =>
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
            });
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
                Task<Scope> scope = LoadScopeAsync(i.Value.URI);
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

        public override string ToString ()
        {
            return $"Library(Units:[{String.Join(',', Units.Select(u => u.Key))}])";
        }
    }
}
