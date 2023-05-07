using Antlr4.Runtime.Misc;
using Compiler.AntlrBuild;
using Compiler.Common.Exceptions;
using Compiler.Parsing.Syntax;
using Compiler.Parsing.Syntax.Values;
using Compiler.Parsing.Syntax.Variables;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {
        public override GlobalVariable VisitGlobalVariableDeclaration ([NotNull] CrimsonParser.GlobalVariableDeclarationContext context)
        {
            CrimsonParser.AssignVariableContext ivdc = context.assignVariable();
            VariableAssignment assignment;

            if (ivdc is CrimsonParser.AssignVariableDirectContext direct) assignment = VisitAssignVariableDirect(direct);
            else if (ivdc is CrimsonParser.AssignVariableAtPointerContext pointer) assignment = VisitAssignVariableAtPointer(pointer);
            else throw new CrimsonParserException("Cannot parse GlobalVariableDeclarationContext with unknown assignment type " + ivdc.GetType());

            return new GlobalVariable(assignment);
        }

        public override ScopeVariable VisitScopeVariableDeclaration ([NotNull] CrimsonParser.ScopeVariableDeclarationContext context)
        {
            FullName name = new FullName(context.name.Text);
            ISimpleValue size = VisitDatasize(context.size);

            return new ScopeVariable(name, size);
        }

        public override object VisitScopeVariableStatement ([NotNull] CrimsonParser.ScopeVariableStatementContext context)
        {
            return VisitScopeVariableDeclaration(context.scopeVariableDeclaration());
        }

        public VariableAssignment ParseAssignVariable ([NotNull] CrimsonParser.AssignVariableContext context)
        {
            if (context == null) throw new StatementParseException("Illegal null CrimsonParser.AssignVariableContext", null, null);
            if (context is CrimsonParser.AssignVariableDirectContext avdc)
                return VisitAssignVariableDirect(avdc);
            else if (context is CrimsonParser.AssignVariableAtPointerContext avapc)
                return VisitAssignVariableAtPointer(avapc);
            else
                throw new StatementParseException("The given CrimsonParser.AssignVariableContext (" + context + ") is not of a permissable type", null, null);
        }

        public override VariableAssignment VisitAssignVariableDirect ([NotNull] CrimsonParser.AssignVariableDirectContext context)
        {
            FullName identifier = new FullName(context.name.Text);
            ISimpleValue size = VisitDatasize(context.size);
            if (context.simple != null) return new VariableAssignment(identifier, size, VisitSimpleValue(context.simple));
            else if (context.complex != null) return new VariableAssignment(identifier, size, VisitComplexValue(context.complex));
            else throw new CrimsonParserException($"Cannot assign no value to variable {identifier}");
        }

        public override VariableAssignment VisitAssignVariableAtPointer ([NotNull] CrimsonParser.AssignVariableAtPointerContext context)
        {
            //TODO AssignVariableAtPointer just adds an asterisk to the variable name
            FullName identifier = new FullName(context.name.Text);
            ISimpleValue size = VisitDatasize(context.size);
            if (context.simple != null) return new VariableAssignment(identifier, size, VisitSimpleValue(context.simple));
            else if (context.complex != null) return new VariableAssignment(identifier, size, VisitComplexValue(context.complex));
            else throw new CrimsonParserException($"Cannot assign no value to variable {identifier}");
        }

    }
}
