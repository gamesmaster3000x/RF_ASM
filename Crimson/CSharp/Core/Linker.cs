using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;
using CrimsonBasic.CSharp.Core;
using System.Linq;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Converts a CompilationUnit to a LinkedUnit.
    /// </summary>
    internal class Linker
    {
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

            // Iterate through each relevant unit
            foreach (KeyValuePair<string, CompilationUnit> pair in compilation.Library.Units)
            {

                // Generate linking context for the current unit (based on the aliases of imports)
                // This means mapping "ALIAS" to "UNIT" so that each statement can remap itself
                LinkingContext ctx = new LinkingContext(pair.Key);
                CompilationUnit unit = pair.Value;
                var statements = GetAllStatements(unit);
                foreach (KeyValuePair<string, Import> importPair in unit.Imports)
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
                    CompilationUnit mappingUnit = compilation.Library.LookupUnitByPath(relativePath);
                    if (mappingUnit == null) throw new LinkingException("Could not add unloaded unit " + relativePath + " (alias=" + alias + ") to mapping context");

                    ctx.Links.Add(alias, mappingUnit);
                }

                // Link each statement in the unit
                foreach (var statement in statements)
                {
                    statement.Link(ctx);
                }
            }
        }

        private static List<CrimsonStatement> GetAllStatements(CompilationUnit unit)
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