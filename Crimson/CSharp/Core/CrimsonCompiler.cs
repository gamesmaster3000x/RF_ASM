using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Statements;
using NLog;

namespace Crimson.CSharp.Core
{
    internal class CrimsonCompiler
    {
        private static Logger LOGGER;
        public CrimsonCmdArguments Options { get; }
        private UnitGenerator UnitGenerator;
        private Linker linker;
        private Dictionary<string, CompilationUnit> units;

        public CrimsonCompiler(CrimsonCmdArguments options, UnitGenerator generator, Linker linker)
        {
            Options = options;
            UnitGenerator = generator;
            linker = linker;
            units = new Dictionary<string, CompilationUnit>();
        }

        public int FullyCompileFromOptions()
        {
            string programText = string.Join(Environment.NewLine, File.ReadLines(Options.CompilationSourcePath));
            CompilationUnit unit = UnitGenerator.GetUnitFromText(programText);
            return 1;
        }

    }
}
