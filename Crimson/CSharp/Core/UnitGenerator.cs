using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;
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
    internal class UnitGenerator
    {

        public static readonly string SYSTEM_LIBRARY_PREFIX = "${NATIVE}";


        private Options Options { get; }
        private Dictionary<string, TranslationUnit> Units { get; }

        public UnitGenerator(Options options)
        {
            Options = options;
            Units = new Dictionary<string, TranslationUnit>();
        }

        public TranslationUnit GetUnitFromPath(string pathIn)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();

            string path = StandardiseNativePath(pathIn);
            TranslationUnit? unit = LookupUnitByPath(path);

            if (unit != null)
            {
                return unit;
            }

            try
            {
                string programText = string.Join(Environment.NewLine, File.ReadLines(path));
                TranslationUnit newUnit = GetUnitFromText(path + " (" + pathIn + ")", programText);
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

        public TranslationUnit GetUnitFromText(string sourceName, string textIn)
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
            TranslationUnit translationUnit = visitor.VisitTranslationUnit(cuCtx);

            return translationUnit;
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

        private TranslationUnit? LookupUnitByPath(string path)
        {
            if (Units.ContainsKey(path))
            {
                return Units[path];
            }
            return null;
        }
    }
}
