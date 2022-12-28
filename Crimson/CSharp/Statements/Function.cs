using Crimson.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Statements
{
    /// <summary>
    /// A function, defined with the function keyword. Is a member of a package.
    /// </summary>
    internal class Function : GlobalStatement
    {

        public Function(CrimsonType returnType, string name, IList<Parameter> parameters, IList<InternalStatement> statements)
        {
            ReturnType = returnType;
            Name = name;
            Parameters = parameters;
            Statements = statements;
        }

        public CrimsonType ReturnType { get; }
        public string Name { get; set; }
        public IList<Parameter> Parameters { get; }
        public IList<InternalStatement> Statements { get; }

        public override void Link(LinkingContext ctx)
        {
            foreach (var p in Parameters)
            {
                p.Link(ctx);
            }

            foreach (var s in Statements)
            {
                s.Link(ctx);
            }
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

            internal void Link(LinkingContext ctx)
            {
                throw new NotImplementedException();
            }
        }
    }
}
