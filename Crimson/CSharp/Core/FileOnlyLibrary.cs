using System;
using System.Collections.Concurrent;
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
    internal class FileOnlyLibrary : ILibrary
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

        public FileOnlyLibrary ()
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

        public Scope LoadScope (Uri uri, bool root)
        {
            // Safety check to prevent double loading
            // Immediately insert the key to reserve it
            Scope scope = GetScope(uri);
            if (scope != null) return scope;

            Units[uri] = null;

            LOGGER.Info($"Loading{(root ? $" root" : "")}: {uri}");

            Stream source = GetStreamOf(uri);
            StreamReader reader = new StreamReader(source);
            string text = reader.ReadToEnd();

            scope = ParseScopeText(uri.ToString(), text);

            // Generate task
            Task<Scope> task = new Task<Scope>(() =>
            {
                Scope scope = LoadScopeFromFile(name).Result;
                LoadScopeDependencies(scope);
                return scope;
            });

            // Handle if root
            if (root)
            {
                if (Units.ContainsKey(ROOT_HOST))
                {
                    throw new NullReferenceException("There cannot be multiple root scopes in a FileOnlyLibrary.");
                }
                Units[ROOT_HOST] = task; // This name is reserved and should be free 
            }

            // Start loading (asynchronously)
            task.Start();
            return task;
        }

        public IEnumerable<Task<Scope>> GetUnits ()
        {
            return Units.Values;
        }

        /// <summary>
        /// Loads dependencies for the given root CompilationUnit, as well as that unit's dependencies, recursively.
        /// Also checks for nested scopes and loads them as well!
        /// </summary>
        /// <param name="root"></param>
        private async void LoadScopeDependencies (Scope root)
        {
            // For each import
            foreach (var i in root.Imports)
            {
                // Get the unit it refers to 
                Scope scope = await LoadScopeAsync(i.Value.Path, false);

                //TODO remove LoadScopeDep. continue
                if (scope == null)
                    continue;

                // Get that units' dependencies (recursively)
                LoadScopeDependencies(scope);
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

        [Obsolete]
        private async Task<Scope> LoadScopeFromFile (string pathIn)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();

            string path = StandardiseNativePath(pathIn);

            if (pathIn.Equals(ROOT_HOST) || pathIn.Equals(NATIVE_HOST))
            {
                throw new UnitGeneratorException("Illegal unit path: Cannot import unit/facet/scope with reserved name '" + pathIn + "'");
            }

            Scope? unit = await GetScope(path);
            if (unit != null)
            {
                return unit;
            }

            LOGGER.Info($"Loading new root Scope from: {pathIn}");
            try
            {
                string programText = string.Join(Environment.NewLine, File.ReadLines(path));
                Scope newUnit = ParseScopeText(path + " (" + pathIn + ")", programText);

                Task<Scope> scope = Task.FromResult(newUnit);
                Units[path] = scope;

                return newUnit;
            }
            catch (IOException io)
            {
                throw new UnitGeneratorException("Unable to find source file for CompilationUnit " + path + " (" + pathIn + ")", io);
            }
            catch (StatementParseException spe)
            {
                throw new UnitGeneratorException("Error while parsing '" + path + "' (" + pathIn + ")", spe);
            }
            catch (System.Exception e)
            {
                throw new UnitGeneratorException("Unexpected error while creating CompilationUnit from " + path + " (" + pathIn + ")", e);
            }
        }

        private Scope ParseScopeText (string sourcePath, string textIn)
        {
            LOGGER.Debug($"Parsing {textIn.Length} characters in {sourcePath} with ANTLR...");
            // Get Antlr context
            AntlrInputStream a4is = new AntlrInputStream(textIn);
            CrimsonLexer lexer = new CrimsonLexer(a4is);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            CrimsonParser parser = new CrimsonParser(cts);

            lexer.AddErrorListener(new LexerErrorListener(sourcePath));
            parser.ErrorHandler = new ParserErrorStrategy(sourcePath);

            CrimsonParser.ScopeContext cuCtx = parser.scope();
            ScopeVisitor visitor = new ScopeVisitor();
            Scope scope = visitor.VisitScope(cuCtx);

            scope.Path = sourcePath;

            return scope;
        }

        private Stream GetStreamOf (Uri uri)
        {
            uri = SquashUri(uri);
            FileStream stream = new FileStream(uri.AbsolutePath, FileMode.Open);
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
                string localPath = Path.Combine(Crimson.Options.NativeLibraryPath, uri.AbsolutePath);
                builder.Host = "";
                builder.Path = localPath;
            }

            // file:///heap.crm
            if (!Path.IsPathRooted(uri.AbsolutePath))
            {
                string? parentDirectory = Path.GetDirectoryName(Crimson.Options.TranslationSourcePath);
                string localPath = Path.Combine(parentDirectory, uri.AbsolutePath);
                builder.Path = localPath;
            }

            return builder.Uri;
        }

        public override string ToString ()
        {
            return $"Library(Units:[{String.Join(',', Units.Select(u => Path.GetFileName(u.Key)))}])";
        }
    }
}
