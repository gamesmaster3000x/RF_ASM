using Crimson.CSharp.Assembly;
using Crimson.CSharp.Grammar.Tokens;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Statements
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

        public override void Link (LinkingContext ctx)
        {
            TargetFunction = LinkerHelper.LinkFunctionCall(FunctionIdentifier, ctx);
        }

        public override Fragment GetCrimsonBasic ()
        {
            return new Fragment(0);
        }
    }
}