using Crimson.CSharp.Grammar;
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
    public class Compilation
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// A library of all of the CompilationUnits used in this Compilation
        /// </summary>
        internal Library Library { get; }

        public Compilation (Scope rootUnit, CrimsonOptions options)
        {
            Library = new Library(options);

            LoadScopeDependencies(rootUnit);
            Library.Units[Library.ROOT_FACET_NAME] = rootUnit; // This name is reserved and should be free
        }

        /// <summary>
        /// Loads dependencies for the given root CompilationUnit, as well as that unit's dependencies, recursively.
        /// Also checks for nested scopes and loads them as well!
        /// </summary>
        /// <param name="root"></param>
        private void LoadScopeDependencies (Scope root)
        {
            // For each import
            foreach (var i in root.Imports)
            {
                // Get the unit it refers to 
                Scope unit = Library.LoadScopeFromFile(i.Value.Path);

                // Get that units' dependencies (recursively)
                LoadScopeDependencies(unit);
            }

            // Check for imports in nested scopes
            foreach (var del in root.Delegates)
            {
                if (del.Invoke() is IHasScope hasScope)
                {
                    LoadScopeDependencies(hasScope.GetScope());
                }
            }
        }

        public Scope GetRootUnit ()
        {
            return Library.Units[Library.ROOT_FACET_NAME];
        }

        public override string ToString ()
        {
            return $"Compilation(RootUnit={GetRootUnit()}; Library={Library.ToString()})";
        }
    }
}