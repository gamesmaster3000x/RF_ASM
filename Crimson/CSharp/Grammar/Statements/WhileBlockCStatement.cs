using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class WhileBlockCStatement : AbstractCrimsonStatement, IHasScope
    {

        public WhileBlockCStatement (ConditionCToken condition, Scope scope)
        {
            Condition = condition;
            Scope = scope;
        }

        public ConditionCToken Condition { get; }
        public Scope Scope { get; }

        public Scope GetScope () => Scope;

        public override void Link (LinkingContext ctx)
        {
            Condition.Link(ctx);
            Scope.Link(ctx);
            Linked = true;
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
        public override Fragment GetCrimsonBasic ()
        {
            Fragment wholeBlock = new Fragment(0);

            wholeBlock.Add(new CommentBStatement(""));
            wholeBlock.Add(new CommentBStatement(" >> TODO IMPLEMENT WHILE (start) <<"));
            wholeBlock.Add(new CommentBStatement(" >> TODO IMPLEMENT WHILE (end) <<"));
            wholeBlock.Add(new CommentBStatement(""));

            return wholeBlock;
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }
    }
}