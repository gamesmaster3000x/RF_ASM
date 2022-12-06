using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class FunctionCall : FunctionOnlyStatement
    {
        private string identifier;
        private IList<FunctionArgument> arguments;

        public FunctionCall(string identifier, IList<FunctionArgument> arguments)
        {
            this.identifier = identifier;
            this.arguments = arguments;
        }
    }
}