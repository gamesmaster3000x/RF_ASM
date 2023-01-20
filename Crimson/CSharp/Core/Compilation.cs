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


        /// <summary>
        /// A library of all of the CompilationUnits used in this Compilation
        /// </summary>
        internal Library Library { get; }

        public Compilation(CompilationUnit rootUnit, CrimsonOptions options)
        {
            Library = new Library(options);

            LoadLibrary(rootUnit);
            Library.Units[Library.ROOT_FACET_NAME] = rootUnit; // This name is reserved and should be free
        }

        /// <summary>
        /// Loads dependencies for the given root CompilationUnit, as well as that unit's dependencies, recursively.
        /// </summary>
        /// <param name="root"></param>
        private void LoadLibrary(CompilationUnit root)
        {
            // For each import
            foreach (var i in root.Imports)
            {
                // Get the unit it refers to 
                CompilationUnit unit = Library.LoadUnitFromPath(i.Value.Path);

                // Get that units' dependencies (recursively)
                LoadLibrary(unit);
            }
        }

        public CompilationUnit GetRootUnit()
        {
            return Library.Units[Library.ROOT_FACET_NAME];
        }

        public override string ToString()
        {
            return $"Compilation(RootUnit={GetRootUnit()}; Library={Library.ToString()})";
        }
    }
}