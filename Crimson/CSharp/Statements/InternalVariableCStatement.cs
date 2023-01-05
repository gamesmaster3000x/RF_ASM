using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class InternalVariable : InternalStatement
    {
        private CrimsonTypeCToken type;
        public string identifier { get; set; }

        public InternalVariable(CrimsonTypeCToken type, string identifier)
        {
            this.type = type;
            this.identifier = identifier;
        }

        public InternalVariable(CrimsonTypeCToken type, string identifier, ResolvableValue? value) : this(type, identifier)
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