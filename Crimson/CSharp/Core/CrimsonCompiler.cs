using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Statements;
using NLog;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// The root of all compilation. Initiates and delegates tasks for the compilation process.
    /// </summary>
    internal class CrimsonCompiler
    {
        private static Logger LOGGER;
        public CrimsonCmdArguments Options { get; }
        public UnitGenerator UnitGenerator { get; }
        public Linker Linker { get; }

        public CrimsonCompiler(CrimsonCmdArguments options, UnitGenerator unitGenerator, Linker linker)
        {
            Options = options;
            UnitGenerator = unitGenerator;
            Linker = linker;
        }

        public int FullyCompileFromOptions()
        {
            // Get the root unit (ie. main.crm)
            CompilationUnit rootUnit = UnitGenerator.GetUnitFromPath(Options.CompilationSourcePath);

            // Generate dependency units (all resources are now accessible)
            Compilation compilation = new Compilation(rootUnit, UnitGenerator);

            // Link FunctionCalls
            LinkedUnit linkedUnit = Linker.Link(compilation);

            /*
             * Now we need to break this list into simple statements - a strange kind of Crimson/RFASM mash-up.
             * This strange intermediate language will have some high-level features from Crimson, but will use a flattened control flow.
             * This basically means that conditions will be replaced with jumps.
             * 
             * >> main.crm
             *  function main () {
             *      int i = 5;
             *      
             *      if (i == 4) {
             *          i = 6;
             *      } else {
             *          return false;
             *      }
             *  }
             * 
             * >> main.crmrfp
             *  ::func_main
             *  int i = 5
             *  
             *  bool b = (i == 4)               // Condition has been extracted
             *  JNE b, true, "not_equal"        // Jump if condition false (ie. i != 4)
             *      i = 6
             *      JMP "end_condition"
             *  ::not_equal                     // This is the else block
             *      return false;
             *  ::end_condition
             *  ::endfunc_main
             */

            return 1;
        }

    }
}
