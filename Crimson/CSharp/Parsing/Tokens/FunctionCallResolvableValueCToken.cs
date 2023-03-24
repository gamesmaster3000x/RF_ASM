using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Statements;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Tokens
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

        public override string ToString ()
        {
            return "Call: " + FunctionCall.Identifier.ToString() + "()";
        }
    }
}