using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Reflection
{
    /// <summary>
    /// A function, defined with the function keyword. Is a member of a package.
    /// </summary>
    internal class Function: CompilationUnitStatement
    {
       public string Name { get; set; }
        Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();
        Dictionary<string, FunctionOnlyStatement> statements = new Dictionary<string, FunctionOnlyStatement>();

        public Function(string text) : base(text)
        {
        }
    }
}
