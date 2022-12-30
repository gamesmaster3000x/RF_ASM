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
            foreach (KeyValuePair<string, CompilationUnit> pair in compilation.Library.Units)
            {
                CompilationUnit unit = pair.Value;
                var statements = GetAllStatements(unit);

                LinkingContext ctx = new LinkingContext(pair.Key);
                foreach (KeyValuePair<string, Import> dependency in unit.Imports)
                {
                    /*
                     * Get the alias of the dependency.
                     * For example:
                     *  The alias of '#using "utils.crm" as u' is 'u'
                     */
                    string alias = dependency.Key;

                    /*
                     * Get the absolute path of the unit which the alias refers to.
                     * This is the path which is used to look up the dependency in a Compilation.
                     * For example:
                     *  '#using "utils.crm" as u' may result in 'C:/utils.crm'
                     */
                    string path = compilation.Library.Units[dependency.Value.Path].ToString(); // the absolute path of the alias
                }

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