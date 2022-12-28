using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class Condition: ICrimsonToken
    {
        public Condition(ResolvableValue value)
        {
            Value = value;
        }

        public ResolvableValue Value { get; }

        public void Link(LinkingContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}