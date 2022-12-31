using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class FunctionCall : InternalStatement
    {
        private string identifier;
        private Function? targetFunction;
        private IList<ResolvableValue> arguments;

        public FunctionCall(string identifier, IList<ResolvableValue> arguments): base()
        {
            this.identifier = identifier;
            this.arguments = arguments;
        }

        public override void Link(LinkingContext ctx)
        {
            if (IsLinked()) return;

            targetFunction = LinkerHelper.LinkFunctionCall(identifier, ctx);

            foreach (var a in arguments)
            {
                a.Link(ctx);
            }

            SetLinked(true);
        }
    }
}