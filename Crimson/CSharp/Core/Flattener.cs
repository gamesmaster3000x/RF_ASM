using Antlr4.Runtime.Misc;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;
using CrimsonBasic.CSharp.Core;
using NLog;
using System.Text.RegularExpressions;

namespace Crimson.CSharp.Core
{
    public class Flattener
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
        public CrimsonOptions Options { get; }

        internal Flattener (CrimsonOptions options)
        {
            Options = options;
        }

        /*
         * - Flattening member names -
         * Each member knows its own name. 
         * All of these are singletons, so updating the name of a member in one location will update it in all locations.
         * When a new member is declared, if the name already exists, it will be given a new name (e.g. 'originalname_1'). 
         * This name will be updated across all locations because the target member is a singleton.
         */
        internal BasicProgram Flatten(Compilation compilation)
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
            foreach (var pair in variables)
            {
                IList<BasicStatement> bs = pair.Value.GetCrimsonBasic();
                program.Statements.Concat(bs);
                LOGGER.Debug($"Added GlobalVariable {pair.Value.Name}");
            }

            // Add structures
            foreach (var pair in structures)
            {
                IList<BasicStatement> bs = pair.Value.GetCrimsonBasic();
                program.Statements.Concat(bs);
                LOGGER.Debug($"Added Structure {pair.Value.Name}");
            }

            // Add main (entry) function
            Function entry = GetEntryFunction(compilation);
            LOGGER.Info($"Found entry Function {entry.Name}");
            IList<BasicStatement> entryBs = entry.GetCrimsonBasic();
            program.Statements.Concat(entryBs);
            LOGGER.Debug($"Added entry Function {entry.Name}");

            // Add remaining functions
            foreach (var pair in functions)
            {
                if (pair.Value == entry) continue;
                IList<BasicStatement> bs = pair.Value.GetCrimsonBasic();
                program.Statements.Concat(bs);
                LOGGER.Debug($"Added Function {pair.Value.Name}");
            }

            return program;
        }

        private Function GetEntryFunction(Compilation compilation)
        {
            string baseName = Options.EntryFunctionName;
            CompilationUnit rootUnit = compilation.GetRootUnit();
            string pattern = $"^{baseName}_[0-9]+$"; //  Match name_090923 (anchored to start and end)
            Regex regex = new Regex(pattern);

            IList<Function> funcs = rootUnit.Functions.Values.Where(func => regex.IsMatch(func.Name)).ToList();
            if (funcs.Count < 1) throw new FlatteningException($"Found {funcs.Count} (exactly 1 required) valid entry methods {funcs} for root unit {rootUnit} of compilation {compilation}");
            if (funcs.Count > 1) throw new FlatteningException($"Multiple ({funcs.Count}) valid entry methods (maximum permissable 1) {funcs} for root unit {rootUnit} of compilation {compilation}");
            Function entry = funcs.Single();
            return entry;
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