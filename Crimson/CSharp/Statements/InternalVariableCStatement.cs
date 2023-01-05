using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class InternalVariableCStatement : InternalStatement
    {
        private CrimsonTypeCToken type;
        public string identifier { get; set; }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier)
        {
            this.type = type;
            this.identifier = identifier;
        }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier, ResolvableValueCToken? value) : this(type, identifier)
        {
            Value = value;
        }

        public ResolvableValueCToken? Value { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }
    }
}