using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class IfBlock : InternalStatement
    {
        public IfBlock()
        {
        }

        public IfBlock(Condition condition, IList<InternalStatement> body, ElseIfBlock? elifBlock, ElseBlock? elseBlock)
        {
            Condition = condition;
            Body = body;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public Condition Condition { get; }
        public IList<InternalStatement> Body { get; }
        public ElseIfBlock? ElifBlock { get; }
        public ElseBlock? ElseBlock { get; }

        public override void Link(LinkingContext ctx)
        {
            Condition.Link(ctx);

            foreach(var s in Body)
            {
                s.Link(ctx);
            }

            ElifBlock?.Link(ctx);
            ElseBlock?.Link(ctx);
        }
    }
}