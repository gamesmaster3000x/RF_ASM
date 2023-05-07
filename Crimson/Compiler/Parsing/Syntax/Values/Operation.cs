using Compiler.Common.Exceptions;
using Compiler.Mapping;

namespace Compiler.Parsing.Syntax.Values
{
    public class Operation : IComplexValue
    {

        public OperationType OpType { get; }
        public ISimpleValue LeftToken { get; }
        public ISimpleValue RightToken { get; }

        public Operation (ISimpleValue leftToken, OperationType opType, ISimpleValue rightToken)
        {
            LeftToken = leftToken;
            OpType = opType;
            RightToken = rightToken;
        }

        public void Map (MappingContext ctx)
        {
            LeftToken.Map(ctx);
            RightToken.Map(ctx);
        }

        public static OperationType ParseOpType (string s)
        {
            s = s.Trim();
            return s switch
            {
                "+" => OperationType.ADD,
                "-" => OperationType.SUB,
                "*" => OperationType.AST,
                "/" => OperationType.SLA,
                "==" => OperationType.EQU,
                "<" => OperationType.LES,
                "<=" => OperationType.LEQ,
                ">" => OperationType.GTR,
                ">=" => OperationType.GEQ,
                "£" => OperationType.POU,
                "$" => OperationType.DOL,
                "%" => OperationType.PER,
                "^" => OperationType.HAT,
                "&" => OperationType.AMP,


                "<<>" => OperationType.LES_TEMP,
                "<>>" => OperationType.GTR_TEMP,
                _ => throw new CrimsonParserException("Illegal operator type '" + s + "'"),
            };
        }

        public enum OperationType
        {
            // Maths
            ADD, SUB, AST, SLA,

            // Comparison
            EQU, LEQ, LES, GEQ, GTR,

            // Misc
            POU, DOL, PER, HAT, AMP,

            // Temp
            LES_TEMP, GTR_TEMP
        }
    }
}