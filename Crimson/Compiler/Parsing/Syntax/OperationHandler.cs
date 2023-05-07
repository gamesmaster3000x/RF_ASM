using Compiler.Mapping;
using Compiler.Parsing.Syntax.Functions;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.Syntax
{
    public class OperationHandler : IMappable
    {
        public Operation.OperationType OpType { get; protected set; }
        public FullName FunctionIdentifier { get; protected set; }
        public Function? TargetFunction { get; protected set; }

        public OperationHandler (Operation.OperationType opType, FullName identifier)
        {
            OpType = opType;
            FunctionIdentifier = identifier;
        }

        public FunctionCall Apply (FunctionArgument arg1, FunctionArgument arg2)
        {
            return null;
        }

        public void Map (MappingContext context)
        {
            TargetFunction = MapperHelper.GetLinkedFunctionForCall(FunctionIdentifier, context);
        }
    }
}