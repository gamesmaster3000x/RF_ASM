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
        public ICollection<Parameter> Parameters1 { get; }
        public ICollection<FunctionOnlyStatement> Statements1 { get; }

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

        public Function(CrimsonType returnType, string name, ICollection<Parameter> parameters1, ICollection<FunctionOnlyStatement> statements1)
        {
            ReturnType = returnType;
            Name = name;
            Parameters1 = parameters1;
            Statements1 = statements1;
        }

        internal class Parameter
        {
            public Parameter(CrimsonType type, string identifier)
            {
                Type1 = type;
                Identifier = identifier;
            }

            public CrimsonType Type1 { get; }
            public string Identifier { get; }
            Type Type { get; set; }
            string Name { get; set; }
        }
    }
}
