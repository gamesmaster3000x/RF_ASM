using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Statements
{
    public class OperationHandlerCStatement: ICrimsonStatement
    {
        public CrimsonTypeCToken Type1 { get; protected set; }
        public OperationResolvableValueCToken.OperationType OpType { get; protected set; }
        public CrimsonTypeCToken Type2 { get; protected set; }
        public FullNameCToken FunctionIdentifier { get; protected set; }
        public FunctionCStatement? TargetFunction { get; protected set; }

        public OperationHandlerCStatement(CrimsonTypeCToken type1, OperationResolvableValueCToken.OperationType opType, CrimsonTypeCToken type2, FullNameCToken identifier)
        {
            Type1 = type1;
            OpType = opType;
            Type2 = type2;
            FunctionIdentifier = identifier;
        }

        public bool CanApply(CrimsonTypeCToken type1, CrimsonTypeCToken type2)
        {
            return false;
        }

        public FunctionCallCStatement Apply(FunctionArgumentCToken arg1, FunctionArgumentCToken arg2)
        {
            return null;
        }

        public void Link(LinkingContext ctx)
        {
            Type1.Link(ctx);
            Type2.Link(ctx);
            TargetFunction = LinkerHelper.LinkFunctionCall(FunctionIdentifier, ctx);
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }

        public Fragment GetCrimsonBasic ()
        {
            throw new NotImplementedException();
        }
    }
}