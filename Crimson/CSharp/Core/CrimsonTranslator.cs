using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Statements;
using NLog;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// The root of all compilation. Initiates and delegates tasks for the compilation process.
    /// </summary>
    internal class CrimsonTranslator
    {
        private static Logger LOGGER;
        public CrimsonOptions Options { get; }
        public UnitGenerator UnitGenerator { get; }
        public Linker Linker { get; }

        public CrimsonTranslator(CrimsonOptions options, UnitGenerator unitGenerator, Linker linker)
        {
            Options = options;
            UnitGenerator = unitGenerator;
            Linker = linker;
        }

        public int FullyCompileFromOptions()
        {
            /*
             * == PARSING STAGE == 
             * 
             * The syntax of the source files must be parsed from text to an abstract syntax tree.
             * In this case, the work is done by ANTLR, using the Crimson.g4 grammar file.
             * This stage results in a collection of individual units which contain ComplexStatements exactly describing the input.
             * 
             */
            TranslationUnit rootUnit = UnitGenerator.GetUnitFromPath(Options.TranslationSourcePath); // Get the root unit (ie. main.crm)
            Translation compilation = new Translation(rootUnit, UnitGenerator); // Generate dependency units (all resources are now accessible)


            /*
             * == FLATTENING STAGE == 
             * 
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


            /*
             * == LINKING STAGE == 
             * 
             * Now that all of the statements have been flattened into one list, we can iterate through and link the FunctionCalls.
             * 
             */
            // Link FunctionCalls
            // LinkedUnit linkedUnit = Linker.Link(compilation);


            /*
             * == TRANSLATION STAGE ==
             * 
             * All statements are now translated to CrimsonBasic statements
             * 
             */


            /*
             * == FURTHER COMPILATION STAGES == 
             * 
             * Depending on specified options, the CrimsonBasic or RFASM compilers may now be invoked.
             * 
             */

            return 1;
        }

    }
}
