using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class FunctionCallResolvableValueCToken : ComplexValueCToken
    {
        public FunctionCallCStatement FunctionCall { get; }

        public FunctionCallResolvableValueCToken(FunctionCallCStatement functionCall)
        {
            FunctionCall = functionCall;
        }

        public override void Link(LinkingContext ctx)
        {
            FunctionCall.Link(ctx);
        }

        public override Fragment GetBasicFragment()
        {
            throw new NotImplementedException();
        }
    }
}