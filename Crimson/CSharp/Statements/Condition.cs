using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class Condition
    {
        public Condition(ResolvableValue value)
        {
            Value = value;
        }

        public ResolvableValue Value { get; }
    }
}