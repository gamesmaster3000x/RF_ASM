using CrimsonCore.Specialising;
using CrimsonCore.Generalising.Structures;
using CrimsonCore.Parsing.Tokens;
using CrimsonCore.Generalising.Structures;
using CrimsonCore.Generalising;
using CrimsonCore.Linking;

namespace CrimsonCore.Parsing.Statements
{
    internal class IfBlockCStatement : AbstractCrimsonStatement, IHasScope
    {
        public IfBlockCStatement (ConditionCToken condition, Scope scope, ElseIfBlockCToken? elifBlock, ElseBlockCToken? elseBlock)
        {
            Condition = condition;
            Scope = scope;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public ConditionCToken Condition { get; }
        public Scope Scope { get; }
        public ElseIfBlockCToken? ElifBlock { get; }
        public ElseBlockCToken? ElseBlock { get; }

        public Scope GetScope () => Scope;

        public override void Link (LinkingContext ctx)
        {
            Condition.Link(ctx);
            Scope.Link(ctx);
            ElifBlock?.Link(ctx);
            ElseBlock?.Link(ctx);
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
        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ScopeAssemblyStructure scope = new ScopeAssemblyStructure();
            scope.AddSubStructure(new CommentAssemblyStructure(""));

            // If
            // Condition
            string uniqueBranchName = "IF_BLOCK_UNIQUE_BRANCH_NAME";
            string endLabelName = "END_" + uniqueBranchName;
            scope.AddSubStructure(new JumpAssemblyStructure("NEXT_ELIF"));
            scope.AddSubStructure(Scope.Generalise(context));
            scope.AddSubStructure(new JumpAssemblyStructure(endLabelName));

            // Elif and/or Else
            /*if (ElifBlock != null)
                scope.AddSubStructure(ElifBlock.G());
            else if (ElseBlock != null)
                scope.AddSubStructure(ElseBlock.GetCrimsonBasic());*/

            // End of if
            scope.AddSubStructure(new LabelAssemblyStructure(endLabelName));
            scope.AddSubStructure(new CommentAssemblyStructure(""));

            return scope;
        }
    }
}