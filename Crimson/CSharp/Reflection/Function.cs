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
        public CrimsonType ReturnType { get; }
        public List<Parameter> Parameters { get; }
        public List<FunctionOnlyStatement> Statements { get; }

        Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();
        Dictionary<string, FunctionOnlyStatement> statements = new Dictionary<string, FunctionOnlyStatement>();

        public Function(string text) : base(text)
        {
        }

        public Function(CrimsonType returnType, string name, List<Parameter> parameters1, List<FunctionOnlyStatement> statements1)
        {
            ReturnType = returnType;
            Name = name;
            Parameters = parameters1;
            Statements = statements1;
        }
    }
}
