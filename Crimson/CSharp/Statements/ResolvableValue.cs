using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class ResolvableValue: ICrimsonToken
    {
        public ResolvableValue(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public void Link(LinkingContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}