using Compiler.AntlrBuild;
using Antlr4.Runtime.Misc;
using Compiler.Parser.Syntax.Values;
using Compiler.Parser.Syntax;
using Compiler.Parser.Syntax.Functions;

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
