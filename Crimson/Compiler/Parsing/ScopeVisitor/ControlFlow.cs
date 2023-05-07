using Compiler.AntlrBuild;
using Antlr4.Runtime.Misc;
using Compiler.Parsing.Syntax;
using Compiler.Parsing.Syntax.Values;
using Compiler.Parsing.Syntax.ControlFlow;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {
        public override Condition VisitCondition ([NotNull] CrimsonParser.ConditionContext context)
        {
            Operation operation = VisitOperation(context.op);
            Condition condition = new Condition(operation);
            return condition;
        }

        public override ElseIfBlock VisitElseIfBlock ([NotNull] CrimsonParser.ElseIfBlockContext context)
        {
            IfBlock ifBlock = VisitIfBlock(context.ifBlock());
            ElseIfBlock elseIfBlock = new ElseIfBlock(ifBlock);
            return elseIfBlock;
        }

        public override ElseBlock VisitElseBlock ([NotNull] CrimsonParser.ElseBlockContext context)
        {
            Scope statements = VisitScope(context.scope());
            ElseBlock elseBlock = new ElseBlock(statements);
            return elseBlock;
        }

        public override Return VisitFunctionReturn ([NotNull] CrimsonParser.FunctionReturnContext context)
        {
            CrimsonParser.SimpleValueContext? rvc = context?.simpleValue();
            ISimpleValue value = rvc == null ? new RawValue("NULL") : VisitSimpleValue(rvc);
            Return ret = new Return(value);
            return ret;
        }

        public override IfBlock VisitIfBlock ([NotNull] CrimsonParser.IfBlockContext context)
        {
            Condition condition = VisitCondition(context.condition());
            Scope scope = VisitScope(context.scope());
            CrimsonParser.ElseIfBlockContext eibCtx = context.elseIfBlock();
            CrimsonParser.ElseBlockContext elbCtx = context.elseBlock();
            ElseIfBlock? elifBlock = eibCtx == null ? null : VisitElseIfBlock(eibCtx);
            ElseBlock? elseBlock = elbCtx == null ? null : VisitElseBlock(elbCtx);
            IfBlock ifBlock = new IfBlock(condition, scope, elifBlock, elseBlock);
            return ifBlock;
        }

        public override WhileBlockCStatement VisitWhileBlock ([NotNull] CrimsonParser.WhileBlockContext context)
        {
            Condition condition = VisitCondition(context.condition());
            Scope body = VisitScope(context.scope());
            WhileBlockCStatement ifBlock = new WhileBlockCStatement(condition, body);
            return ifBlock;
        }
    }
}
