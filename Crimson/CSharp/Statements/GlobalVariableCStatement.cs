using Crimson.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Statements
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    public class GlobalVariable : GlobalStatement
    {
        private CrimsonTypeCToken type;

        public GlobalVariable(CrimsonTypeCToken type, string identifier)
        {
            this.type = type;
            Name = identifier;
        }

        public GlobalVariable(CrimsonTypeCToken type, string identifier, ResolvableValue? value) : this(type, identifier)
        {
            Value = value;
        }

        public ResolvableValue? Value { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }
    }
}
