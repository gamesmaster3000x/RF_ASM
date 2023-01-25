using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class OperationResolvableValueCToken : ComplexValueCToken
    {

        public OperationType OpType { get; }
        public ComplexValueCToken LeftToken { get; }
        public ComplexValueCToken RightToken { get; }

        public OperationResolvableValueCToken(ComplexValueCToken leftToken, OperationType opType, ComplexValueCToken rightToken)
        {
            LeftToken = leftToken;
            OpType = opType;
            RightToken = rightToken;
        }

        public override void Link(LinkingContext ctx)
        {
            LeftToken.Link(ctx);
            RightToken.Link(ctx);
        }

        public static OperationType ParseOpType(string s)
        {
            s = s.Trim();
            switch (s)
            {
                case "+":
                    return OperationType.ADD;
                case "-":
                    return OperationType.SUB;
                case "*":
                    return OperationType.MUL;
                case "/":
                    return OperationType.DIV;
            }
            throw new ParserException("Illegal operator type '" + s + "'");
        }

        public override Fragment GetBasicFragment()
        {
            throw new NotImplementedException();
        }

        public enum OperationType
        {
            ADD, SUB, MUL, DIV
        }
    }
}