using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class OperationResolvableValueCToken : ComplexValueCToken
    {

        public OperationType OpType { get; }
        public SimpleValueCToken LeftToken { get; }
        public SimpleValueCToken RightToken { get; }

        public OperationResolvableValueCToken(SimpleValueCToken leftToken, OperationType opType, SimpleValueCToken rightToken)
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
            return s switch
            {
                "+" => OperationType.ADD,
                "-" => OperationType.SUB,
                "*" => OperationType.MUL,
                "/" => OperationType.DIV,
                "==" => OperationType.EQU,
                "<" => OperationType.LES,
                "<=" => OperationType.LEQ,
                ">" => OperationType.GTR,
                ">=" => OperationType.GEQ,
                _ => throw new CrimsonParserException("Illegal operator type '" + s + "'"),
            };
        }

        public override Fragment GetBasicFragment()
        {
            Fragment fragment = new Fragment(0);
            fragment.Add(new CommentBStatement("Operation"));
            return fragment;
        }

        public enum OperationType
        {
            // Maths
            ADD, SUB, MUL, DIV,

            // Comparison
            EQU, LEQ, LES, GEQ, GTR
        }
    }
}