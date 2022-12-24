using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// Converts a CompilationUnit to a LinkedUnit.
    /// </summary>
    internal class Linker
    {
        public CrimsonCmdArguments Options { get; }
        public UnitGenerator UnitGenerator { get; }
        public Linker(CrimsonCmdArguments options, UnitGenerator generator)
        {
            Options = options;
            UnitGenerator = generator;
        }

        /// <summary>
        /// Converts a CompilationUnit to a LinkedUnit.
        /// </summary>
        /// <param name="root"></param>
        /// <returns>The linked LinkedUnit resulting from the root CompilationUnit.</returns>
        public LinkedUnit Link(CompilationUnit root)
        {
            // Copy non-linkable features to output
            LinkedUnit linkedUnit = new LinkedUnit(root.Functions, root.Structures, root.GlobalVariables);

            // Link imports
            foreach (Import import in root.Imports)
            {
                // Get unit
                string importRelativePath = import.Path;
                string? rootDirectory = Path.GetDirectoryName(Options.CompilationSourcePath);
                string combinedPath = Path.Combine(rootDirectory, importRelativePath); //TODO Linker is causing errors when combining paths (stops discovery of native due to hidden prefix)
                CompilationUnit unit = UnitGenerator.GetUnitFromPath(combinedPath);

                // Link unit (recursively)
                LinkedUnit lu = Link(unit);

                // Combine the root linkedUnit with the results of this import
                linkedUnit.CombineWith(lu);
            }

            // This linkedUnit contains the results of all imports, and all of the imports' imports (recursively)
            return linkedUnit;
        }
    }
}