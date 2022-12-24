using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;
using System;
using System.Collections.Generic;
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

        private CrimsonCmdArguments Options { get; }
        public UnitGenerator(CrimsonCmdArguments options)
        {
            Options = options;
        }

        public CompilationUnit GetUnitFromPath(string pathIn)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();

            string path = pathIn;
            if (pathIn.StartsWith(SYSTEM_LIBRARY_PREFIX))
            {
                path = pathIn.Replace(SYSTEM_LIBRARY_PREFIX, Options.NativeLibraryPath);
            }

            if (!File.Exists(path))
            {
                throw new UnitGeneratorException("Unable to create CompilationUnit for non-existent file at " + path + " (" + pathIn + ")");
            }

            string programText = string.Join(Environment.NewLine, File.ReadLines(path));
            return GetUnitFromText(programText);
        }

        public CompilationUnit GetUnitFromText(string textIn)
        {
            // Get Antlr context
            AntlrInputStream a4is = new AntlrInputStream(textIn);
            CrimsonLexer lexer = new CrimsonLexer(a4is);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            CrimsonParser parser = new CrimsonParser(cts);

            CrimsonParser.CompilationUnitContext cuCtx = parser.compilationUnit();
            CrimsonCompiliationUnitVisitor visitor = new CrimsonCompiliationUnitVisitor();
            CompilationUnit compilation = visitor.VisitCompilationUnit(cuCtx);

            return compilation;
        }
    }
}
