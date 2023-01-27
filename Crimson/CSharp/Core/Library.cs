using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Generates CompilationUnits from input text with the power of ANTLR.
    /// </summary>
    internal class Library
    {

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
        internal Dictionary<string, CompilationUnit> Units { get; }

        public Library(CrimsonOptions options)
        {
            Options = options;
            Units = new Dictionary<string, CompilationUnit>();
        }

        public CompilationUnit LoadUnitFromPath(string pathIn)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();

            string path = StandardiseNativePath(pathIn);

            if (pathIn.Equals(ROOT_FACET_NAME) || pathIn.Equals(SYSTEM_LIBRARY_PREFIX))
            {
                throw new UnitGeneratorException("Illegal unit path: Cannot import unit/facet with reserved name '" + pathIn + "'");
            }

            CompilationUnit? unit = LookupUnitByPath(path);
            if (unit != null)
            {
                return unit;
            }

            try
            {
                string programText = string.Join(Environment.NewLine, File.ReadLines(path));
                CompilationUnit newUnit = LoadUnitFromText(path + " (" + pathIn + ")", programText);
                Units[path] = newUnit;
                return newUnit;
            } 
            catch (IOException io) 
            {
                throw new UnitGeneratorException("Unable to find source file for CompilationUnit " + path + " (" + pathIn + ")", io);
            } 
            catch (System.Exception e) 
            {
                throw new UnitGeneratorException("Error while creating CompilationUnit from " + path + " (" + pathIn + ")", e);
            }
        }

        public CompilationUnit LoadUnitFromText(string sourceName, string textIn)
        {
            // Get Antlr context
            AntlrInputStream a4is = new AntlrInputStream(textIn);
            CrimsonLexer lexer = new CrimsonLexer(a4is);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            CrimsonParser parser = new CrimsonParser(cts);

            lexer.AddErrorListener(new LexerErrorListener(sourceName));
            parser.ErrorHandler = new ParserErrorStrategy(sourceName);

            CrimsonParser.TranslationUnitContext cuCtx = parser.translationUnit();
            CrimsonCompiliationUnitVisitor visitor = new CrimsonCompiliationUnitVisitor();
            CompilationUnit compilationUnit = visitor.VisitTranslationUnit(cuCtx);

            return compilationUnit;
        }

        public string StandardiseNativePath(string path)
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

        public CompilationUnit? LookupUnitByPath(string path)
        {
            if (Units.ContainsKey(path))
            {
                return Units[path];
            } else
            {
                var nativePath = StandardiseNativePath(path);
                if (Units.ContainsKey(nativePath))
                {
                    return Units[nativePath];
                }
            }
            return null;
        }

        public override string ToString()
        {
            return $"Library(Units={Units})";
        }
    }
}
