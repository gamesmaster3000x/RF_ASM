using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crimson.CSharp.Reflection;

namespace Crimson.Statements
{
    /// <summary>
    /// A package contains functions and global variables. They may be imported and used by other packages. 
    /// 
    /// Names of contents are parsed eagerly. Values of contents are generated lazily.
    /// </summary>
    internal class LazyPackage
    {
        public LazyPackage(string v, string r)
        {
        }

        /// <summary>
        /// The name of this package, for example: "crimson.utils".
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// The names of the packages which this package depends on, for example: { "system.console", "utils.crm" }.
        /// </summary>
        internal List<string> Dependencies { get; set; }

        /// <summary>
        /// The global variables in this package.
        /// </summary>
        internal Dictionary<string, Function> GlobalVariable { get; set; }

        /// <summary>
        /// The functions in this package.
        /// </summary>
        internal Dictionary<string, Function> Functions { get; set; }
    }
}
