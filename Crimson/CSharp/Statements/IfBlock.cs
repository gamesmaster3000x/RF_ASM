using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class IfBlock : InternalStatement
    {
        public IfBlock()
        {
        }

        public IfBlock(Condition condition, IList<InternalStatement> body, ElifBlock elifBlock, ElseBlock elseBlock)
        {
            Condition = condition;
            Body = body;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public Condition Condition { get; }
        public IList<InternalStatement> Body { get; }
        public ElifBlock ElifBlock { get; }
        public ElseBlock ElseBlock { get; }
    }
}