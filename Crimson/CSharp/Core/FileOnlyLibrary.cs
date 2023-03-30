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

        public static readonly string SYSTEM_LIBRARY_PREFIX = "${NATIVE}";
        public static readonly string ROOT_FACET_NAME = "${ROOT}";

        /// <summary>
        /// The "master" list of units. Stored so that each unique unit is only created once.
        /// 
        /// For example:
        /// [ C:/utils.crm => UTILS_UNIT ]
        /// [ C:/main.crm  => MAIN_UNIT  ]
        /// </summary>
        public ConcurrentDictionary<string, Task<Scope>> Units { get; }

        public FileOnlyLibrary()
        {
            Units = new ConcurrentDictionary<string, Task<Scope>>();
        }

        public Task<Scope> GetScope(string path)
        {
            if (Units.ContainsKey(path))
            {
                return Units[path];
            }
            else
            {
                var nativePath = StandardiseNativePath(path);
                if (Units.ContainsKey(nativePath))
                {
                    return Units[nativePath]; 
                }
            }
            return Task.FromResult<Scope>(null!);
        }

        public Task<Scope> LoadScopeAsync(string path, bool root)
        {
            // Safety check to prevent double loading
            // Immediately insert the key to reserve it
            if(Units.ContainsKey(path)) return Units[path];
            Units[path] = Task.FromResult<Scope>(null!);

            LOGGER.Info($"Async loading{(root ? $" root" : "")}: {path}");

            // Generate task
            Task<Scope> task = new Task<Scope>(() => {
                Scope scope = LoadScopeFromFile(path).Result;
                LoadScopeDependencies(scope);
                return scope;
            });

            // Handle if root
            if (root)
            {
                if (Units.ContainsKey(ROOT_FACET_NAME))
                {
                    throw new NullReferenceException("There cannot be multiple root scopes in a FileOnlyLibrary.");
                }
                Units[ROOT_FACET_NAME] = task; // This name is reserved and should be free 
            }

            // Start loading (asynchronously)
            task.Start();
            return task;
        }

        public IEnumerable<Task<Scope>> GetUnits()
        {
            return Units.Values;
        }

        /// <summary>
        /// Loads dependencies for the given root CompilationUnit, as well as that unit's dependencies, recursively.
        /// Also checks for nested scopes and loads them as well!
        /// </summary>
        /// <param name="root"></param>
        private void LoadScopeDependencies (Scope root)
        {
            // For each import
            foreach (var i in root.Imports)
            {
                // Get the unit it refers to 
                Task<Scope> task = LoadScopeAsync(i.Value.Path, false);
                task.Wait();

                // Get that units' dependencies (recursively)
                LoadScopeDependencies(task.Result);
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

        private async Task<Scope> LoadScopeFromFile(string pathIn)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();

            string path = StandardiseNativePath(pathIn);

            if (pathIn.Equals(ROOT_FACET_NAME) || pathIn.Equals(SYSTEM_LIBRARY_PREFIX))
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

        private Scope ParseScopeText(string sourcePath, string textIn)
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

        private string StandardiseNativePath(string path)
        {
            if (path.StartsWith(SYSTEM_LIBRARY_PREFIX))
            {
                string result = Path.GetFullPath(path.Replace(SYSTEM_LIBRARY_PREFIX, Crimson.Options.NativeLibraryPath));
                return result;
            }
            if (!Path.IsPathRooted(path))
            {
                string? parentDirectory = Path.GetDirectoryName(Crimson.Options.TranslationSourcePath);
                path = Path.Combine(parentDirectory, path);
            }
            return path;
        }

        public override string ToString()
        {
            return $"Library(Units:[{String.Join(',', Units.Select(u => Path.GetFileName(u.Key)))}])";
        }
    }
}
