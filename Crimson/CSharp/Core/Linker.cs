using Antlr4.Runtime.Misc;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar;
using Crimson.CSharp.Grammar.Statements;
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
        public Linker (CrimsonOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// Links the FunctionCalls in a Compilation.
        /// </summary>
        /// <param name="compilation"></param>
        public void Link (Compilation compilation)
        {
            LOGGER.Info("Linking compilation " + compilation);

            // Iterate through each relevant unit
            foreach (KeyValuePair<string, Scope> keyScopePair in compilation.Library.Units)
            {
                LOGGER.Info("Linking " + keyScopePair);

                // Generate linking context for the current unit (based on the aliases of imports)
                // This means mapping "ALIAS" to "UNIT" so that each statement can remap itself
                LinkingContext ctx = new LinkingContext(keyScopePair.Key, keyScopePair.Key, new Dictionary<string, Scope>(), compilation);
                Scope unit = keyScopePair.Value;
                ctx.Links.Add(keyScopePair.Key, keyScopePair.Value); //TODO This may cause issues

                // Add links from the current unit
                Dictionary<string, Scope> NewLinks = keyScopePair.Value.GetLinks(compilation);
                foreach (var l in NewLinks)
                {
                    ctx.Links.Add(l.Key, l.Value);
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

        private static List<AbstractCrimsonStatement> GetAllStatements (Scope unit)
        {
            var statements = new List<AbstractCrimsonStatement>();
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