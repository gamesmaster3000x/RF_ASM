using Crimson.CSharp.Reflection;
using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    internal class IfBlock: FunctionOnlyStatement
    {
        public IfBlock()
        {
        }

        public IfBlock(Condition condition, IList<FunctionOnlyStatement> body, ElifBlock elifBlock, ElseBlock elseBlock)
        {
            Condition = condition;
            Body = body;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public Condition Condition { get; }
        public IList<FunctionOnlyStatement> Body { get; }
        public ElifBlock ElifBlock { get; }
        public ElseBlock ElseBlock { get; }
    }
}