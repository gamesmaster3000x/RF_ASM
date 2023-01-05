using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class FunctionCallCStatement : InternalStatement
    {
        private string identifier;
        private FunctionCStatement? targetFunction;
        private IList<ResolvableValueCToken> arguments;

        public FunctionCallCStatement(string identifier, IList<ResolvableValueCToken> arguments): base()
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