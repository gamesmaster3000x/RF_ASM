using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Reflection
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    internal class GlobalVariable: CompilationUnitStatement
    {
        public GlobalVariable(string text) : base(text)
        {
        }

        public string Name { get; set; }
    }
}
