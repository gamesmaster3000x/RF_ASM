using Crimson.CSharp.Statements;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Core
{
    public class Flattener
    {

        /*
         * - Flattening member names -
         * Each member knows its own name. 
         * All of these are singletons, so updating the name of a member in one location will update it in all locations.
         * When a new member is declared, if the name already exists, it will be given a new name (e.g. 'originalname_1'). 
         * This name will be updated across all locations because the target member is a singleton.
         */
        internal BasicProgram Flatten(Compilation compilation, CrimsonOptions options)
        {
            BasicProgram program = new BasicProgram();

            Dictionary<string, Function> functions = new Dictionary<string, Function>();
            Dictionary<string, Structure> structures = new Dictionary<string, Structure>();
            Dictionary<string, GlobalVariable> variables = new Dictionary<string, GlobalVariable>();

            /*
             * Create 3 universal lists which contain all of the statements.
             * These have already been dynamically mapped (they know which singletons each call refers to).
             * During collection, these values are reassigned names (which are globally updated) to avoid name clashes.
             */
            foreach (KeyValuePair<string, CompilationUnit> pair in compilation.Library.Units)
            {
                foreach (var f in pair.Value.Functions)
                {
                    FixNameAndAdd(functions, f.Value);
                }
                foreach (var s in pair.Value.Structures)
                {
                    FixNameAndAdd(structures, s.Value);
                }
                foreach (var g in pair.Value.GlobalVariables)
                {
                    FixNameAndAdd(variables, g.Value);
                }
            }

            // Add global variables
            // Add structures
            // Add main (entry) function
            // Add remaining functions

            return program;
        }

        private void FixNameAndAdd<GS>(Dictionary<string, GS> map, GS gs) where GS: GlobalStatement
        {
            int i = 0;
            while (map.ContainsKey(gs.Name + "_" + i))
            {
                i++;
            }
            gs.Name += $"_{i}";
            map.Add(gs.Name, gs);
        }
    }
}