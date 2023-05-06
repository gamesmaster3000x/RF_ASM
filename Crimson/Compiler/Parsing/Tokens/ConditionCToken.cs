using Compiler.Parsing.Tokens.Values;
using Compiler.Mapping;

namespace Compiler.Parsing.Tokens
{
    internal class ConditionCToken : ICrimsonToken
    {
        public OperationResolvableValueCToken Operation { get; }

        public ConditionCToken (OperationResolvableValueCToken operation)
        {
            Operation = operation;
        }

        public void Link (MappingContext ctx)
        {
            Operation.Link(ctx);
        }
    }
}