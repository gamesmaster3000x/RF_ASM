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

        public static readonly string SYSTEM_LIBRARY_PREFIX = "${NATIVE}";
        public static readonly string ROOT_FACET_NAME = "${ROOT}";

        private CrimsonOptions Options { get; }

        /// <summary>
        /// The "master" list of units. Stored so that each unique unit is only created once.
        /// 
        /// For example:
        /// [ C:/utils.crm => UTILS_UNIT ]
        /// [ C:/main.crm  => MAIN_UNIT  ]
        /// </summary>
        internal Dictionary<string, Scope> Units { get; }

        public Library (CrimsonOptions options)
        {
            Options = options;
            Units = new Dictionary<string, Scope>();
        }

        public Scope LoadScopeFromFile (string pathIn)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();

            string path = StandardiseNativePath(pathIn);

            if (pathIn.Equals(ROOT_FACET_NAME) || pathIn.Equals(SYSTEM_LIBRARY_PREFIX))
            {
                throw new UnitGeneratorException("Illegal unit path: Cannot import unit/facet/scope with reserved name '" + pathIn + "'");
            }

            Scope? unit = LookupScopeByPath(path);
            if (unit != null)
            {
                return unit;
            }

            LOGGER.Info($"Loading new root Scope from: {pathIn}");
            try
            {
                string programText = string.Join(Environment.NewLine, File.ReadLines(path));
                Scope newUnit = ParseScopeText(path + " (" + pathIn + ")", programText);
                Units[path] = newUnit;
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

        public Scope ParseScopeText (string sourceName, string textIn)
        {
            LOGGER.Debug($"Parsing {textIn.Length} characters in {sourceName} with ANTLR...");
            // Get Antlr context
            AntlrInputStream a4is = new AntlrInputStream(textIn);
            CrimsonLexer lexer = new CrimsonLexer(a4is);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            CrimsonParser parser = new CrimsonParser(cts);

            lexer.AddErrorListener(new LexerErrorListener(sourceName));
            parser.ErrorHandler = new ParserErrorStrategy(sourceName);

            CrimsonParser.ScopeContext cuCtx = parser.scope();
            ScopeVisitor visitor = new ScopeVisitor();
            Scope compilationUnit = visitor.VisitScope(cuCtx);

            return compilationUnit;
        }

        public string StandardiseNativePath (string path)
        {
            if (path.StartsWith(SYSTEM_LIBRARY_PREFIX))
            {
                string result = Path.GetFullPath(path.Replace(SYSTEM_LIBRARY_PREFIX, Options.NativeLibraryPath));
                return result;
            }
            if (!Path.IsPathRooted(path))
            {
                string? parentDirectory = Path.GetDirectoryName(Options.TranslationSourcePath);
                path = Path.Combine(parentDirectory, path);
            }
            return path;
        }

        public Scope? LookupScopeByPath (string path)
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
            return null;
        }

        public override string ToString ()
        {
            return $"Library(Units:[{String.Join(',', Units.Select(u => Path.GetFileName(u.Key)))}])";
        }
    }
}
