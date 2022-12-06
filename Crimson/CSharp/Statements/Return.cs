using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class Return : FunctionOnlyStatement
    {
        public Return(ResolvableValue value)
        {
            Value = value;
        }

        public ResolvableValue Value { get; }
    }
}