using Crimson.CSharp.Assembly;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class FunctionCallResolvableValueCToken : ComplexValueCToken
    {
        public FunctionCallCStatement FunctionCall { get; }

        public FunctionCallResolvableValueCToken (FunctionCallCStatement functionCall)
        {
            FunctionCall = functionCall;
        }

        public override void Link (LinkingContext ctx)
        {
            FunctionCall.Link(ctx);
        }

        public override Fragment GetBasicFragment ()
        {
            Fragment fragment = new Fragment(0);
            fragment.Add(new CommentBStatement("Function Call"));
            return fragment;
        }
    }
}