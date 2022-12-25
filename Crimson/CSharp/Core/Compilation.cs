using Crimson.CSharp.Statements;
using NLog;

namespace Crimson.CSharp.Core
{
    internal class Compilation
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public CompilationUnit RootUnit { get; }
        internal UnitGenerator UnitGenerator { get; }
        internal Dictionary<string, CompilationUnit> Dependencies { get; }

        public Compilation(CompilationUnit rootUnit, UnitGenerator unitGenerator)
        {
            RootUnit = rootUnit;
            UnitGenerator = unitGenerator;
            Dependencies = FindDependencies(rootUnit);
        }

        private Dictionary<string, CompilationUnit> FindDependencies(CompilationUnit root)
        {
            var dependencies = new Dictionary<string, CompilationUnit>();

            // For each import
            foreach (var i in root.Imports)
            {
                // Get the unit it refers to 
                CompilationUnit unit = UnitGenerator.GetUnitFromPath(i.Value.Path);

                // Get that units' dependencies (recursively)
                var internalDependencies = FindDependencies(unit);
                internalDependencies.Add(i.Value.Path, unit);

                // Add each dependency if it is not already added
                foreach (var dependency in internalDependencies)
                {
                    if (dependencies.ContainsKey(i.Value.Path))
                    {
                        LOGGER.Info("Duplicate dependency " + i.Value.Path + " called " + i.Value.Alias);
                    }
                    else
                    {
                        dependencies[dependency.Key] = dependency.Value;
                    }
                    dependencies[i.Value.Path] = unit;
                }
            }
            
            return dependencies;
        }
    }
}