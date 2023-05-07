using Compiler.Mapping;
using Compiler.Generalising;
using Compiler.Generalising.Structures;

namespace Compiler.Parsing.Syntax.ControlFlow
{
    internal class IfBlock : IAssemblable, IHasScope, IMappable
    {
        public IfBlock (Condition condition, Scope scope, ElseIfBlock? elifBlock, ElseBlock? elseBlock)
        {
            Condition = condition;
            Scope = scope;
            ElifBlock = elifBlock;
            ElseBlock = elseBlock;
        }

        public Condition Condition { get; }
        public Scope Scope { get; }
        public ElseIfBlock? ElifBlock { get; }
        public ElseBlock? ElseBlock { get; }

        public Scope GetScope () => Scope;

        public void Map (MappingContext ctx)
        {
            Condition.Map(ctx);
            Scope.Map(ctx);
            ElifBlock?.Map(ctx);
            ElseBlock?.Map(ctx);
        }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
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

            // End of if
            scope.AddSubStructure(new LabelAssemblyStructure(endLabelName));
            scope.AddSubStructure(new CommentAssemblyStructure(""));

            return scope;
        }
    }
}