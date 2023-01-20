using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class WhileBlockCStatement : InternalStatement
    {

        public WhileBlockCStatement(ConditionCToken condition, IList<InternalStatement> body)
        {
            Condition = condition;
            Body = body;
        }

        public ConditionCToken Condition { get; }
        public IList<InternalStatement> Body { get; }

        public override void Link(LinkingContext ctx)
        {
            Condition.Link(ctx);

            foreach(var s in Body)
            {
                s.Link(ctx);
            }
        }

        /*
         * if condition {
         *  1
         * } else if condition {
         *  2
         * } else {
         *  3
         * }
         * 
         * 
         * bool A = condition
         * JNE A, 1 END_A
         *  (1)
         *  JMP END_IF
         * :END_A
         * 
         * bool B = condition
         * JNE B, 1 END_B
         *  (2)
         *  JMP END_IF
         * :END_B
         * 
         *  (3)
         * :END_IF
         */
        public override Fragment GetCrimsonBasic()
        {
            Fragment wholeBlock = new Fragment(0);

            wholeBlock.Add(new CommentBStatement(""));
            wholeBlock.Add(new CommentBStatement(" >> TODO IMPLEMENT WHILE (start) <<"));
            wholeBlock.Add(new CommentBStatement(" >> TODO IMPLEMENT WHILE (end) <<"));
            wholeBlock.Add(new CommentBStatement(""));

            return wholeBlock;
        }
    }
}