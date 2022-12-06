namespace Crimson.CSharp.Reflection
{
    internal class ResolvableValue
    {
        public ResolvableValue(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }
}