using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class Return : InternalStatement
    {
        public Return(ResolvableValueCToken value)
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