﻿
using Compiler.Mapping;
using Compiler.Parsing.Tokens;
using Compiler.Parsing;
using Compiler.Generalising;
using Compiler.Generalising.Structures;

namespace Compiler.Parsing.Statements
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

        public override void Link (MappingContext ctx)
        {
            Condition.Link(ctx);
            Scope.Link(ctx);
            Mapped = true;
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

            scope.AddSubStructure(new CommentAssemblyStructure(" >> TODO IMPLEMENT WHILE (start) <<"));
            scope.AddSubStructure(new CommentAssemblyStructure(" >> TODO IMPLEMENT WHILE (end) <<"));

            return scope;
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }
    }
}