using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Core
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