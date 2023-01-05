using Antlr4.Runtime.Misc;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;
using CrimsonBasic.CSharp.Core;
using NLog;
using System.Linq;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Converts a CompilationUnit to a LinkedUnit.
    /// </summary>
    internal class Linker
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public CrimsonOptions Options { get; }
        public Linker(CrimsonOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// Links the FunctionCalls in a Compilation.
        /// </summary>
        /// <param name="compilation"></param>
        public void Link(Compilation compilation)
        {
            LOGGER.Info("Linking compilation " + compilation);

            // Iterate through each relevant unit
            foreach (KeyValuePair<string, CompilationUnitCStatement> pair in compilation.Library.Units)
            {
                LOGGER.Info("Linking KeyValuePair<string, CompilationUnit> " + pair);

                // Generate linking context for the current unit (based on the aliases of imports)
                // This means mapping "ALIAS" to "UNIT" so that each statement can remap itself
                LinkingContext ctx = new LinkingContext(pair.Key, pair.Key, new Dictionary<string, CompilationUnitCStatement>());
                CompilationUnitCStatement unit = pair.Value;
                foreach (KeyValuePair<string, ImportCStatement> importPair in unit.Imports)
                {
                    /*
                     * Get the alias of the dependency.
                     * For example:
                     *  The alias of '#using "utils.crm" as u' is 'u'
                     */
                    string alias = importPair.Key;

                    /*
                     * Get the relative path of the unit which the alias refers to.
                     * For example:
                     *  '#using "utils.crm" as u' may result in 'C:/utils.crm'
                     */
                    string relativePath = importPair.Value.Path;

                    /*
                     * 
                     */
                    CompilationUnitCStatement? mappingUnit = compilation.Library.LookupUnitByPath(relativePath);
                    if (mappingUnit == null) throw new LinkingException("Could not add unloaded unit " + relativePath + " (alias=" + alias + ") to mapping context");

                    ctx.Links.Add(alias, mappingUnit);
                }

                // Link each statement in the unit
                var statements = GetAllStatements(unit);
                foreach (var statement in statements)
                {
                    statement.Link(ctx);
                }
                LOGGER.Debug($"Linked {statements.Count} top-level statements");

                // Just so that I can put a breakpoint here
                continue;
            }

            return;
        }

        private static List<CrimsonStatement> GetAllStatements(CompilationUnitCStatement unit)
        {
            var statements = new List<CrimsonStatement>();
            foreach (var s in unit.Functions.Values)
            {
                statements.Add(s);
            }
            foreach (var s in unit.Structures.Values)
            {
                statements.Add(s);
            }
            foreach (var s in unit.GlobalVariables.Values)
            {
                statements.Add(s);
            }
            return statements;
        }
    }
}