using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class Return : InternalStatement
    {
        public Return(ResolvableValue value)
        {
            Value = value;
        }

        public ResolvableValue Value { get; }
    }
}