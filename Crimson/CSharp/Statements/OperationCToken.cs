using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class OperationCToken : ResolvableValueCToken
    {

        public OpType opType { get; }

        public OperationCToken(OpType opType): base(STRING_CONTENT, ValueType.OPERATION)
        {
            this.opType = opType;
        }

        public override Fragment GetCrimsonBasic()
        {
            throw new NotImplementedException();
        }

        public void Link(LinkingContext ctx)
        {
            throw new NotImplementedException();
        }

        public enum OpType
        {
            ADD, SUB, MUL, DIV
        }
    }
}