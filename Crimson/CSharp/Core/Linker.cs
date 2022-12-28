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
        public Linker(CrimsonOptions options, UnitGenerator generator)
        {
            Options = options;
        }

        /// <summary>
        /// Links the FunctionCalls in a Compilation.
        /// </summary>
        /// <param name="library"></param>
        public void Link(Compilation library)
        {
            foreach (KeyValuePair<string, CompilationUnit> pair in library.Units)
            {
                CompilationUnit unit = pair.Value;
                var statements = GetAllStatements(unit);

                LinkingContext ctx = new LinkingContext();
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