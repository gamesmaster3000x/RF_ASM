using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using static System.Formats.Asn1.AsnWriter;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class IfBlockCStatement : InternalStatement
    {
        public IfBlockCStatement()
        {
        }

        public IfBlockCStatement(ConditionCToken condition, ScopeCToken scope, ElseIfBlockCToken? elifBlock, ElseBlockCToken? elseBlock)
        {
            Condition = condition;
            Scope = scope;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public ConditionCToken Condition { get; }
        public ScopeCToken Scope { get; }
        public ElseIfBlockCToken? ElifBlock { get; }
        public ElseBlockCToken? ElseBlock { get; }

        public override void Link(LinkingContext ctx)
        {
            Condition.Link(ctx);
            Scope.Link(ctx);
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

            wholeBlock.Add(new CommentBStatement(""));

            // If
            // Condition
            Fragment condition = Condition.GetCrimsonBasic();
            Fragment ifHead = new Fragment(0);
            string uniqueBranchName = FlattenerHelper.GetUniqueBranchName();
            string endLabelName = "END_" + uniqueBranchName;
            ifHead.Add(new JumpEqualBStatement(condition.ResultHolder!, "0", "NEXT_ELIF"));
            Fragment ifBody = new Fragment(1);
            ifBody.Add(Scope.GetCrimsonBasic());
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
            wholeBlock.Add(new CommentBStatement(""));

            return wholeBlock;
        }
    }
}