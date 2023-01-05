using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class ReturnCStatement : InternalStatement
    {
        public ReturnCStatement(ResolvableValueCToken value)
        {
            Value = value;
        }

        public ResolvableValueCToken Value { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }
    }
}