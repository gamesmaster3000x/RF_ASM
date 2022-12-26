namespace Crimson.CSharp.Statements
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