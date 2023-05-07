using Compiler.Mapping;
using Compiler.Parsing.Syntax.Functions;

namespace Compiler.Parsing.Syntax.Values
{
    public class FunctionCallValue : IComplexValue
    {
        public FunctionCall FunctionCall { get; }

        public FunctionCallValue (FunctionCall functionCall)
        {
            FunctionCall = functionCall;
        }

        public void Map (MappingContext ctx)
        {
            FunctionCall.Map(ctx);
        }

        public override string ToString ()
        {
            return "Call: " + FunctionCall.Identifier.ToString() + "()";
        }
    }
}