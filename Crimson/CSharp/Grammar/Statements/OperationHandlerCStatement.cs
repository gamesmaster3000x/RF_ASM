using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Statements
{
    public class OperationHandlerCStatement
    {
        public CrimsonTypeCToken Type1 { get; }
        public OperationResolvableValueCToken.OperationType OpType { get; }
        public CrimsonTypeCToken Type2 { get; }
        public string Identifier { get; }

        public OperationHandlerCStatement(CrimsonTypeCToken type1, OperationResolvableValueCToken.OperationType opType, CrimsonTypeCToken type2, string identifier)
        {
            Type1 = type1;
            OpType = opType;
            Type2 = type2;
            Identifier = identifier;
        }

        public bool CanApply(CrimsonTypeCToken type1, CrimsonTypeCToken type2)
        {
            return false;
        }

        public FunctionCallCStatement Apply(FunctionArgumentCToken arg1, FunctionArgumentCToken arg2)
        {
            return null;
        }
    }
}