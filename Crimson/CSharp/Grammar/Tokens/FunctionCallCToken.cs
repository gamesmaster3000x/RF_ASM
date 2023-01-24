using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class FunctionCallCToken : ResolvableValueCToken
    {

        public OpType opType { get; }
        public ResolvableValueCToken LeftToken { get; }
        public OpType T { get; }
        public ResolvableValueCToken RightToken { get; }

        public OperationCToken(ResolvableValueCToken leftToken, OpType t, ResolvableValueCToken rightToken)
        {
            LeftToken = leftToken;
            T = t;
            RightToken = rightToken;
        }

        public override Fragment GetCrimsonBasic()
        {
            throw new NotImplementedException();
        }

        public void Link(LinkingContext ctx)
        {
            throw new NotImplementedException();
        }

        public static OpType ParseOpType(string s)
        {
            s = s.Trim();
            switch (s)
            {
                case "+":
                    return OpType.ADD;
                case "-":
                    return OpType.SUB;
                case "*":
                    return OpType.MUL;
                case "/":
                    return OpType.DIV;
            }
            throw new ParserException("Illegal operator type '" + s + "'");
        }

        public enum OpType
        {
            ADD, SUB, MUL, DIV
        }
    }
}