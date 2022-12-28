using Crimson.CSharp.Statements;
using CrimsonBasic.CSharp.Core;
using NLog;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// A collection of CompilationUnit keyed with their absolute path within the file system.
    /// This is the result of parsing.
    /// The next stage is linking.
    /// 
    /// For example: <"C:/main.crm", CompilationUnit>.
    /// </summary>
    internal class Compilation
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        internal UnitGenerator UnitGenerator { get; }
        internal Dictionary<string, CompilationUnit> Units { get; }

        public Compilation(CompilationUnit rootUnit, UnitGenerator unitGenerator)
        {
            Units = new Dictionary<string, CompilationUnit>();
            UnitGenerator = unitGenerator;

            Units = FindDependencies(rootUnit);
            Units[UnitGenerator.ROOT_FACET_NAME] = rootUnit; // This name is reserved and should be free
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