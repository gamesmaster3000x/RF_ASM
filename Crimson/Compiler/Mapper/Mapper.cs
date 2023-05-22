using NLog;
using Compiler.Common;
using Compiler.Parser.Syntax;

namespace Compiler.Mapper
{
    /// <summary>
    /// Converts a CompilationUnit to a LinkedUnit.
    /// </summary>
    internal class Mapper
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        // TODO Linking must include renaming functions/variables and reach down to identifiers simple values etc.

        /// <summary>
        /// Links the FunctionCalls in a Compilation.
        /// </summary>
        /// <param name="compilation"></param>
        /// 
        [Obsolete]
        public async Task Link (Compilation compilation)
        {
            LOGGER.Info("Linking compilation " + compilation);

            // Iterate through each relevant unit
            foreach (Scope scope in await compilation.Library.GetScopes())
            {
                LOGGER.Info("Linking " + scope);

                // Generate linking context for the current unit (based on the aliases of imports)
                // This means mapping "ALIAS" to "UNIT" so that each statement can remap itself
                MappingContext ctx = new MappingContext(scope, new Dictionary<string, Scope>(), compilation);

                // Add links from the current unit
                scope.Map(ctx);

                // Just so that I can put a breakpoint here
                continue;
            }

            return;
        }

        internal void Map (Compilation compilation)
        {
            throw new NotImplementedException();
        }
    }
}