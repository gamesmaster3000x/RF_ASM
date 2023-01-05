using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class IfBlockCStatement : InternalStatement
    {
        public IfBlockCStatement()
        {
        }

        public IfBlockCStatement(ConditionCToken condition, IList<InternalStatement> body, ElseIfBlock? elifBlock, ElseBlockCToken? elseBlock)
        {
            Condition = condition;
            Body = body;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public ConditionCToken Condition { get; }
        public IList<InternalStatement> Body { get; }
        public ElseIfBlock? ElifBlock { get; }
        public ElseBlockCToken? ElseBlock { get; }

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