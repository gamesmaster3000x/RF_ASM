using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Converts a CompilationUnit to a LinkedUnit.
    /// </summary>
    internal class Linker
    {
        public CrimsonCmdArguments Options { get; }
        public Linker(CrimsonCmdArguments options, UnitGenerator generator)
        {
            Options = options;
        }

        /// <summary>
        /// Converts a CompilationUnit to a LinkedUnit.
        /// </summary>
        /// <param name="root"></param>
        /// <returns>The linked LinkedUnit resulting from the root CompilationUnit.</returns>
        public LinkedUnit Link(Compilation compilation)
        {
            // Set the entry function for the LinkedUnit
            LinkedUnit linked = new LinkedUnit();
            linked.EntryFunction = compilation.RootUnit.Functions[Options.EntryFunctionName];

            // Copy all functions, structures and global variables as-is (they need no changes)
            linked.CopyAllFrom(compilation.RootUnit);

            // TODO Now need to copy other functions, while editing function calls in the proces...

            return null;
        }
    }
}