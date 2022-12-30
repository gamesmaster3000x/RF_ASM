using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class Return : InternalStatement
    {
        public Return(ResolvableValue value)
        {
            Value = value;
        }

        public ResolvableValue Value { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }
    }
}