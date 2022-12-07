namespace Crimson.CSharp.Statements
{
    internal class FunctionCall : InternalStatement
    {
        private string identifier;
        private IList<ResolvableValue> arguments;

        public FunctionCall(string identifier, IList<ResolvableValue> arguments)
        {
            this.identifier = identifier;
            this.arguments = arguments;
        }
    }
}