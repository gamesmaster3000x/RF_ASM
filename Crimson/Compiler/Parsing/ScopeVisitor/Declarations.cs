using Compiler.AntlrBuild;
using Compiler.Parsing.Syntax;
using Compiler.Parsing.Syntax.Values;
using Compiler.Parsing.Syntax.Functions;
using Antlr4.Runtime.Misc;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {
        public override OperationHandler VisitOperationHandler ([NotNull] CrimsonParser.OperationHandlerContext context)
        {
            Operation.OperationType opType = Operation.ParseOpType(context.op.Text);
            FullName id = VisitFullName(context.identifier);
            OperationHandler ohsc = new OperationHandler(opType, id);

            return ohsc;
        }

        public override Function VisitFunctionDeclaration ([NotNull] CrimsonParser.FunctionDeclarationContext context)
        {
            Function.Header header = VisitFunctionHeader(context.header);
            Scope statements = VisitScope(context.body);
            return new Function(header, statements);

        }
    }
}
