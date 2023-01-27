using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Statements
{
    public class OperationHandlerCStatement: GlobalCStatement
    {
        public CrimsonTypeCToken Type1 { get; }
        public OperationResolvableValueCToken.OperationType OpType { get; }
        public CrimsonTypeCToken Type2 { get; }
        public FunctionCStatement.Header Header { get; }

        public OperationHandlerCStatement(CrimsonTypeCToken type1, OperationResolvableValueCToken.OperationType opType, CrimsonTypeCToken type2, FunctionCStatement.Header header)
        {
            Type1 = type1;
            OpType = opType;
            Type2 = type2;
            Header = header;
        }

        public bool CanApply(CrimsonTypeCToken type1, CrimsonTypeCToken type2)
        {
            return false;
        }

        public FunctionCallCStatement Apply(FunctionArgumentCToken arg1, FunctionArgumentCToken arg2)
        {
            return null;
        }

        public override void Link(LinkingContext ctx)
        {
            Type1.Link(ctx);
            Type2.Link(ctx);
            ((ICrimsonToken)Header).Link(ctx);
        }
    }
}