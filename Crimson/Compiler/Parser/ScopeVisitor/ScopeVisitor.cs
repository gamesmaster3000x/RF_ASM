using Antlr4.Runtime.Misc;
using Compiler.AntlrBuild;
using NLog;
using System.Text.RegularExpressions;
using Compiler.Common.Exceptions;
using Compiler.Parsing.Syntax.Variables;
using Compiler.Common;
using Compiler.Parser.Syntax.Values;
using Compiler.Parser.Syntax;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor : CrimsonBaseVisitor<object>
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        // ==================== VISITOR ====================

        public static readonly Stack<Scope> scopeStack = new Stack<Scope>();

        private static readonly Regex URI_SCHEME_FIXER = new Regex(@"");

        public override Scope VisitScope ([NotNull] CrimsonParser.ScopeContext context)
        {
            try
            {
                Scope scope = new Scope(null!, scopeStack.TryPeek(out Scope? result) ? result : null);
                scopeStack.Push(scope);

                // Visit imports
                IList<CrimsonParser.ImportUnitContext> importCtxs = context._imports;
                foreach (CrimsonParser.ImportUnitContext importCtx in importCtxs)
                {
                    FullName id = VisitFullName(importCtx.fullName());
                    Import import = new Import(importCtx.uri.Text, id);
                    scope.Imports.Add(id.ToString(), import);
                }

                // Add operation handlers
                IList<CrimsonParser.OperationHandlerContext> operationHandlersCtxs = context._opHandlers;
                //foreach (CrimsonParser.OperationHandlerContext unitStatementCtx in operationHandlersCtxs) //TODO Operation handlers ScopeVisitor
                //    OperationHandlerCStatement unitStatement = VisitOperationHandler(unitStatementCtx);

                // Visit Compilation-Unit statements
                IList<CrimsonParser.StatementContext> unitStatementCtxs = context._statements;
                foreach (CrimsonParser.StatementContext unitStatementCtx in unitStatementCtxs)
                {
                    IMappable unitStatement = ParseStatement(unitStatementCtx);
                    scope.AddStatement(unitStatement);
                }

                // Populate output fields

                return scopeStack.Pop();
            }
            catch (Exception ex)
            {
                Panicker.Panic($"{GetType()} was unable to parse the given {(context == null ? "NULL" : context.GetType())}", PanicCode.COMPILE_PARSE_SCOPE, ex);
                throw;
            }
        }

        private IMappable ParseStatement (CrimsonParser.StatementContext stCtx)
        {

            if (stCtx is CrimsonParser.GlobalVariableStatementContext globalContext)
            {
                CrimsonParser.GlobalVariableDeclarationContext declaration = globalContext.globalVariableDeclaration();
                return VisitGlobalVariableDeclaration(declaration);
            }
            else if (stCtx is CrimsonParser.ScopeVariableStatementContext scopeVarContext)
            {
                CrimsonParser.ScopeVariableDeclarationContext declaration = scopeVarContext.scopeVariableDeclaration();
                return VisitScopeVariableDeclaration(declaration);
            }
            else if (stCtx is CrimsonParser.FunctionDeclarationStatementContext functionContext)
            {
                CrimsonParser.FunctionDeclarationContext declaration = functionContext.functionDeclaration();
                return VisitFunctionDeclaration(declaration);
            }
            else if (stCtx is CrimsonParser.MaskDeclarationStatementContext maskContext)
            {
                CrimsonParser.MaskDeclarationContext declaration = maskContext.maskDeclaration();
                return ParseMask(declaration);
            }
            else if (stCtx is CrimsonParser.ReturnStatementContext returnContext)
            {
                CrimsonParser.FunctionReturnContext rtnCtx = returnContext.functionReturn();
                return VisitFunctionReturn(rtnCtx);
            }
            else if (stCtx is CrimsonParser.AssignVariableStatementContext assignContext)
            {
                CrimsonParser.AssignVariableContext asvCtx = assignContext.assignVariable();
                return ParseAssignVariable(asvCtx);
            }
            else if (stCtx is CrimsonParser.FunctionCallStatementContext callContext)
            {
                CrimsonParser.FunctionCallContext fncCtx = callContext.functionCall();
                return VisitFunctionCall(fncCtx);
            }
            else if (stCtx is CrimsonParser.IfStatementContext ifContext)
            {
                CrimsonParser.IfBlockContext ifCtx = ifContext.ifBlock();
                return VisitIfBlock(ifCtx);
            }
            else if (stCtx is CrimsonParser.WhileStatementContext whileContext)
            {
                CrimsonParser.WhileBlockContext whileCtx = whileContext.whileBlock();
                return VisitWhileBlock(whileCtx);
            }
            else if (stCtx is CrimsonParser.AssemblyCallStatementContext assContext)
            {
                CrimsonParser.AssemblyCallContext acCtx = assContext.assemblyCall();
                return VisitAssemblyCall(acCtx);
            }
            else
                throw new StatementParseException("The given CrimsonParser.FunctionStatementContext " + stCtx + " is not of a permissable type", null, null);
        }

        public override Operation VisitOperation ([NotNull] CrimsonParser.OperationContext context)
        {
            ISimpleValue leftToken = VisitSimpleValue(context.leftValue);
            Operation.OperationType t = Operation.ParseOpType(context.@operator.Text);
            ISimpleValue rightToken = VisitSimpleValue(context.rightValue);

            Operation oct = new Operation(leftToken, t, rightToken);
            return oct;
        }

        public override AssemblyCall VisitAssemblyCall ([NotNull] CrimsonParser.AssemblyCallContext context)
        {
            string assemblyText = context.assemblyText.Text;
            AssemblyCall call = new AssemblyCall(assemblyText.Replace("\"", ""));
            return call;
        }


    }
}
