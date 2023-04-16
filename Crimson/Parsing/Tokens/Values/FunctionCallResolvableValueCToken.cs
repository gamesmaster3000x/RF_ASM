using Crimson.Exceptions;
using Crimson.Generalising.Structures;
using Crimson.Specialising;
using Crimson.Linking;
using Crimson.Parsing.Statements;

namespace Crimson.Parsing.Tokens.Values
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