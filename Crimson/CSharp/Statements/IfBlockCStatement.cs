using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class IfBlockCStatement : InternalStatement
    {
        public IfBlockCStatement()
        {
        }

        public IfBlockCStatement(ConditionCToken condition, IList<InternalStatement> body, ElseIfBlockCToken? elifBlock, ElseBlockCToken? elseBlock)
        {
            Condition = condition;
            Body = body;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public ConditionCToken Condition { get; }
        public IList<InternalStatement> Body { get; }
        public ElseIfBlockCToken? ElifBlock { get; }
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

            // If
            // Condition
            Fragment condition = Condition.GetCrimsonBasic();
            Fragment ifHead = new Fragment(0);
            string uniqueBranchName = FlattenerHelper.GetUniqueBranchName();
            string endLabelName = "END_" + uniqueBranchName;
            ifHead.Add(new JumpNotEqualBStatement("CONDITION", "1", "NEXT_ELIF"));
            Fragment ifBody = new Fragment(1);
            foreach (var s in Body) 
            {
                ifBody.Add(s.GetCrimsonBasic()); 
            }
            Fragment ifFoot = new Fragment(1);
            ifFoot.Add(new JumpBStatement(endLabelName));

            wholeBlock.Add(condition);
            wholeBlock.Add(ifHead);
            wholeBlock.Add(ifBody);
            wholeBlock.Add(ifFoot);

            // Elif and/or Else
            if (ElifBlock != null) 
                wholeBlock.Add(ElifBlock.GetCrimsonBasic());
            else if (ElseBlock != null) 
                wholeBlock.Add(ElseBlock.GetCrimsonBasic());

            // End of if
            wholeBlock.Add(new LabelBStatement(endLabelName));

            return wholeBlock;
        }
    }
}