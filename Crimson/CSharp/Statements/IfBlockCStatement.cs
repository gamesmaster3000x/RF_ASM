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
            Fragment fragment = new Fragment(0);

            // If
            Fragment conditionStatements = Condition.GetCrimsonBasic().WithIndentation(1);
            fragment.Add(conditionStatements);
            string uniqueBranchName = FlattenerHelper.GetUniqueBranchName();
            string endLabelName = "END_" + uniqueBranchName;
            fragment.Add(new JumpNotEqualBStatement("CONDITION", "1", "NEXT_ELIF"));
            foreach (var s in Body) fragment.Add(s.GetCrimsonBasic());
            fragment.Add(new JumpBStatement(endLabelName));

            // Elif and/or Else
            if (ElifBlock != null) fragment.Add(ElifBlock.GetCrimsonBasic());
            else if (ElseBlock != null) fragment.Add(ElseBlock.GetCrimsonBasic());

            // End of if
            fragment.Add(new LabelBStatement(endLabelName));

            return fragment;
        }
    }
}