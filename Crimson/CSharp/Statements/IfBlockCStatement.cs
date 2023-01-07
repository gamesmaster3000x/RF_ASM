using Crimson.CSharp.Core;
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
        public override IList<BasicStatement> GetCrimsonBasic()
        {
            List<BasicStatement> statements = new List<BasicStatement>();

            // If
            IList<BasicStatement> conditionStatements = Condition.GetCrimsonBasic();
            statements.AddRange(conditionStatements);
            string uniqueBranchName = FlattenerHelper.GetUniqueBranchName();
            string endLabelName = "END_" + uniqueBranchName;
            statements.Add(new JumpNotEqualBStatement("CONDITION", "1", "NEXT_ELIF"));
            statements.Add(new JumpBStatement(endLabelName));

            // Elif and/or Else
            if (ElifBlock != null) statements.AddRange(ElifBlock.GetCrimsonBasic());
            else if (ElseBlock != null) statements.AddRange(ElseBlock.GetCrimsonBasic());

            // End of if
            statements.Add(new LabelBStatement(endLabelName));

            return statements;
        }
    }
}