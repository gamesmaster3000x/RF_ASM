using Antlr4.Runtime.Misc;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    internal class CrimsonCompiliationUnitVisitor: CrimsonBaseVisitor<object>
    {

        public override CompilationUnit VisitTranslationUnit([NotNull] CrimsonParser.TranslationUnitContext context)
        {
            CompilationUnit compilation = new CompilationUnit();

            // Visit imports
            IList<CrimsonParser.ImportUnitContext> importCtxs = context._imports;
            foreach (CrimsonParser.ImportUnitContext importCtx in importCtxs)
            {
                string path = importCtx.path.Text;
                if (path.StartsWith("\"") && path.EndsWith("\""))
                {
                    path = path.Substring(1, path.Length - 2);
                }
                string id = importCtx.identifier.Text;
                ImportCStatement import = new ImportCStatement(path, id);
                compilation.AddImport(import);
            }

            // Visit Compilation-Unit statements
            IList<CrimsonParser.GlobalStatementContext> unitStatementCtxs = context._statements;
            foreach (CrimsonParser.GlobalStatementContext unitStatementCtx in unitStatementCtxs)
            {
                GlobalCStatement unitStatement = ParseGlobalStatement(unitStatementCtx);
                compilation.AddStatement(unitStatement);
            }

            // Populate output fields

            return compilation;
        }

        // ----------------------------------------------------
        // -------------------------- GLOBAL STATEMENTS
        // ----------------------------------------------------

        private GlobalCStatement ParseGlobalStatement(CrimsonParser.GlobalStatementContext context)
        {
            if (context is CrimsonParser.GlobalVariableUnitStatementContext)
            {
                CrimsonParser.GlobalVariableUnitStatementContext gvCtx = (CrimsonParser.GlobalVariableUnitStatementContext)context;
                CrimsonParser.GlobalVariableDeclarationContext declaration = gvCtx.globalVariableDeclaration();
                return VisitGlobalVariableDeclaration(declaration);
            } 
            else if (context is CrimsonParser.FunctionUnitStatementContext)
            {
                CrimsonParser.FunctionUnitStatementContext fnCtx = (CrimsonParser.FunctionUnitStatementContext)context;
                CrimsonParser.FunctionDeclarationContext declaration = fnCtx.functionDeclaration();
                return VisitFunctionDeclaration(declaration);
            } 
            else if (context is CrimsonParser.StructureUnitStatementContext)
            {
                CrimsonParser.StructureUnitStatementContext structureCtx = (CrimsonParser.StructureUnitStatementContext)context;
                CrimsonParser.StructureDeclarationContext declaration = structureCtx.structureDeclaration();
                return VisitStructureDeclaration(declaration);
            } else
            {
                throw new StatementParseException("The given CrimsonParser.CompilationUnitStatementContext " + context + " is not of a permissable type");
            }
        }

        public override GlobalVariableCStatement VisitGlobalVariableDeclaration([NotNull] CrimsonParser.GlobalVariableDeclarationContext context)
        {
            CrimsonParser.InternalVariableDeclarationContext ivdc = context.internalVariableDeclaration();
            CrimsonTypeCToken type = VisitType(ivdc.type());
            string identifier = ivdc.Identifier().GetText();
            ResolvableValueCToken? value = ivdc.resolvableValue() == null ? null : VisitResolvableValue(ivdc.resolvableValue());
            GlobalVariableCStatement variable = new GlobalVariableCStatement(type, identifier, value);
            return variable;
        }

        public override FunctionCStatement VisitFunctionDeclaration([NotNull] CrimsonParser.FunctionDeclarationContext context)
        {
            string name = context.name.Text;
            CrimsonTypeCToken returnType = VisitType(context.returnType);
            IList<InternalStatement> statements = VisitFunctionBody(context.body);
            IList<FunctionCStatement.Parameter> parameters = VisitParameterList(context.parameters); 
            return new FunctionCStatement(returnType, name, parameters, statements);

        }

        public override StructureCStatement VisitStructureDeclaration([NotNull] CrimsonParser.StructureDeclarationContext context)
        {
            string identifier = context.Identifier().GetText();
            IList<InternalStatement> body = VisitStructureBody(context.structureBody());
            StructureCStatement structure = new StructureCStatement(identifier, body);
            return structure;
        }

        public override IList<InternalStatement> VisitStructureBody([NotNull] CrimsonParser.StructureBodyContext context)
        {
            IList<InternalStatement> statements = new List<InternalStatement>();
            foreach (CrimsonParser.InternalVariableDeclarationContext ivdCtx in context.internalVariableDeclaration())
            {
                InternalVariableCStatement var = VisitInternalVariableDeclaration(ivdCtx);
                statements.Add(var);
            }
            return statements;
        }

        public override ResolvableValueCToken VisitResolvableValue([NotNull] CrimsonParser.ResolvableValueContext context)
        {
            return new ResolvableValueCToken(null);
        }

        public override CrimsonTypeCToken VisitType([NotNull] CrimsonParser.TypeContext context)
        {
            return new CrimsonTypeCToken(context.GetText());
        }

        public override IList<FunctionCStatement.Parameter> VisitParameterList([NotNull] CrimsonParser.ParameterListContext context)
        {
            List<FunctionCStatement.Parameter> parameters = new List<FunctionCStatement.Parameter>();
            foreach (CrimsonParser.ParameterContext paCxt in context.parameter())
            {
                CrimsonTypeCToken type = VisitType(paCxt.type());
                string identifier = paCxt.Identifier().GetText();
                FunctionCStatement.Parameter parameter = new FunctionCStatement.Parameter(type, identifier);
                parameters.Add(parameter);
            }
            return parameters;
        }

        public override IList<InternalStatement> VisitFunctionBody([NotNull] CrimsonParser.FunctionBodyContext context)
        {
            List<InternalStatement> statements = new List<InternalStatement>();
            foreach (CrimsonParser.InternalStatementContext stCtx in context._statements)
            {
                InternalStatement statement = ParseInternalStatement(stCtx);
                statements.Add(statement);
            }
            return statements;
        }

        // ----------------------------------------------------
        // -------------------------- INTERNAL STATEMENTS
        // ----------------------------------------------------

        private InternalStatement ParseInternalStatement(CrimsonParser.InternalStatementContext stCtx)
        {
            if(stCtx is CrimsonParser.FunctionVariableDeclarationStatementContext)
            {
                CrimsonParser.FunctionVariableDeclarationStatementContext context = (CrimsonParser.FunctionVariableDeclarationStatementContext)stCtx;
                CrimsonParser.InternalVariableDeclarationContext ivdCtx = context.internalVariableDeclaration();
                return VisitInternalVariableDeclaration(ivdCtx);
            }
            else if (stCtx is CrimsonParser.FunctionReturnStatementContext)
            {
                CrimsonParser.FunctionReturnStatementContext context = (CrimsonParser.FunctionReturnStatementContext)stCtx;
                CrimsonParser.FunctionReturnContext rtnCtx = context.functionReturn();
                return VisitFunctionReturn(rtnCtx);
            }
            else if (stCtx is CrimsonParser.FunctionAssignVariableStatementContext)
            {
                CrimsonParser.FunctionAssignVariableStatementContext context = (CrimsonParser.FunctionAssignVariableStatementContext)stCtx;
                CrimsonParser.AssignVariableContext asvCtx = context.assignVariable();
                return VisitAssignVariable(asvCtx);
            }
            else if (stCtx is CrimsonParser.FunctionAllocateMemoryStatementContext)
            {
                CrimsonParser.FunctionAllocateMemoryStatementContext context = (CrimsonParser.FunctionAllocateMemoryStatementContext)stCtx;
                CrimsonParser.AllocateMemoryContext almCtx = context.allocateMemory();
                return VisitAllocateMemory(almCtx);
            }
            else if (stCtx is CrimsonParser.FunctionFunctionCallStatementContext)
            {
                CrimsonParser.FunctionFunctionCallStatementContext context = (CrimsonParser.FunctionFunctionCallStatementContext)stCtx;
                CrimsonParser.FunctionCallContext fncCtx = context.functionCall();
                return VisitFunctionCall(fncCtx);
            }
            else if (stCtx is CrimsonParser.FunctionIfStatementContext)
            {
                CrimsonParser.FunctionIfStatementContext context = (CrimsonParser.FunctionIfStatementContext)stCtx;
                CrimsonParser.IfBlockContext ifCtx = context.ifBlock();
                return VisitIfBlock(ifCtx);
            }
            else if (stCtx is CrimsonParser.FunctionAssemblyCallStatementContext)
            {
                CrimsonParser.FunctionAssemblyCallStatementContext context = (CrimsonParser.FunctionAssemblyCallStatementContext)stCtx;
                CrimsonParser.AssemblyCallContext acCtx = context.assemblyCall();
                return VisitAssemblyCall(acCtx);
            }
            else
            {
                throw new StatementParseException("The given CrimsonParser.FunctionStatementContext " + stCtx + " is not of a permissable type");
            }
        }

        public override InternalVariableCStatement VisitInternalVariableDeclaration([NotNull] CrimsonParser.InternalVariableDeclarationContext context)
        {
            CrimsonTypeCToken type = VisitType(context.type());
            string identifier = context.Identifier().GetText();
            ResolvableValueCToken? value = context.resolvableValue() == null ? null : VisitResolvableValue(context.resolvableValue());
            InternalVariableCStatement variable = new InternalVariableCStatement(type, identifier, value);
            return variable;
        }

        public override FunctionCallCStatement VisitFunctionCall([NotNull] CrimsonParser.FunctionCallContext context)
        {
            string identifier = context.Identifier().GetText();
            IList<ResolvableValueCToken> arguments = VisitArguments(context.arguments());
            FunctionCallCStatement call = new FunctionCallCStatement(identifier, arguments);
            return call;
        }

        public override IfBlockCStatement VisitIfBlock([NotNull] CrimsonParser.IfBlockContext context)
        {
            ConditionCToken condition = VisitCondition(context.condition());
            IList<InternalStatement> body = VisitFunctionBody(context.functionBody());
            CrimsonParser.ElseIfBlockContext eibCtx = context.elseIfBlock();
            CrimsonParser.ElseBlockContext elbCtx = context.elseBlock();
            ElseIfBlock? elifBlock = eibCtx == null ? null : VisitElseIfBlock(eibCtx);
            ElseBlockCToken? elseBlock = elbCtx == null ? null : VisitElseBlock(elbCtx);
            IfBlockCStatement ifBlock = new IfBlockCStatement(condition, body, elifBlock, elseBlock);
            return ifBlock;
        }

        public override VariableAssignmentCStatement VisitAssignVariable([NotNull] CrimsonParser.AssignVariableContext context)
        {
            string identifier = context.Identifier().GetText();
            ResolvableValueCToken value = VisitResolvableValue(context.resolvableValue());
            VariableAssignmentCStatement assignment = new VariableAssignmentCStatement(identifier, value);
            return assignment;
        }

        public override ReturnCStatement VisitFunctionReturn([NotNull] CrimsonParser.FunctionReturnContext context)
        {
            ResolvableValueCToken value = VisitResolvableValue(context.resolvableValue());
            ReturnCStatement ret = new ReturnCStatement(value);
            return ret;
        }

        public override AssemblyCallCStatement VisitAssemblyCall([NotNull] CrimsonParser.AssemblyCallContext context)
        {
            string assemblyText = context.assemblyText.Text;
            AssemblyCallCStatement call = new AssemblyCallCStatement(assemblyText);
            return call;
        }

        public override MemoryAllocationCStatement VisitAllocateMemory([NotNull] CrimsonParser.AllocateMemoryContext context)
        {
            string identifier = context.Identifier().GetText();
            string numberText = context.Number().GetText();
            int number;
            try
            {
                number = Int32.Parse(numberText);
            }
            catch (FormatException f)
            {
                throw new StatementParseException("Failed to parse string->int " + numberText + " while allocating memory for " + identifier, f);
            }
            MemoryAllocationCStatement allocation = new MemoryAllocationCStatement(identifier, number);
            return allocation;
        }

        public override IList<ResolvableValueCToken> VisitArguments([NotNull] CrimsonParser.ArgumentsContext context)
        {
            IList<ResolvableValueCToken> arguments = new List<ResolvableValueCToken>();
            foreach (CrimsonParser.ResolvableValueContext rvlCxt in context.resolvableValue())
            {
                ResolvableValueCToken value = VisitResolvableValue(rvlCxt);
                arguments.Add(value);
            }
            return arguments;
        }

        public override ConditionCToken VisitCondition([NotNull] CrimsonParser.ConditionContext context)
        {
            ResolvableValueCToken value = VisitResolvableValue(context.resolvableValue());
            ConditionCToken condition = new ConditionCToken(value);
            return condition;
        }

        public override ElseIfBlock VisitElseIfBlock([NotNull] CrimsonParser.ElseIfBlockContext context)
        {
            IfBlockCStatement ifBlock = VisitIfBlock(context.ifBlock());
            ElseIfBlock elseIfBlock = new ElseIfBlock(ifBlock);
            return elseIfBlock;
        }

        public override ElseBlockCToken VisitElseBlock([NotNull] CrimsonParser.ElseBlockContext context)
        {
            IList<InternalStatement> statements = VisitFunctionBody(context.functionBody());
            ElseBlockCToken elseBlock = new ElseBlockCToken(statements);
            return elseBlock;
        }
    }
}
