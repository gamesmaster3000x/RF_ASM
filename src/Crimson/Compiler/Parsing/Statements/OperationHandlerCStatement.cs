using Compiler.Generalising.Structures;

using Compiler.Mapping;
using Compiler.Parsing.Tokens;
using Compiler.Generalising;
using Compiler.Parsing.Tokens.Values;

namespace Compiler.Parsing.Statements
{
    public class OperationHandlerCStatement : AbstractCrimsonStatement
    {
        public OperationResolvableValueCToken.OperationType OpType { get; protected set; }
        public FullNameCToken FunctionIdentifier { get; protected set; }
        public FunctionCStatement? TargetFunction { get; protected set; }

        public OperationHandlerCStatement (OperationResolvableValueCToken.OperationType opType, FullNameCToken identifier)
        {
            OpType = opType;
            FunctionIdentifier = identifier;
        }

        public FunctionCallCStatement Apply (FunctionArgumentCToken arg1, FunctionArgumentCToken arg2)
        {
            return null;
        }

        public override void Link (MappingContext ctx)
        {
            TargetFunction = MapperHelper.LinkFunctionCall(FunctionIdentifier, ctx);
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            return new EmptyAssemblyStructure();
        }
    }
}