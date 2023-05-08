using Compiler.Mapper;
using Compiler.Parser.Syntax.Functions;

namespace Compiler.Parser.Syntax.Values
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