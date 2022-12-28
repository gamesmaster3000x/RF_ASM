using Crimson.CSharp.Core;

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

        public override void Link(LinkingContext ctx)
        {
            identifier = LinkerHelper.LinkIdentifier(identifier, ctx);

            foreach (var a in arguments)
            {
                a.Link(ctx);
            }
        }
    }
}