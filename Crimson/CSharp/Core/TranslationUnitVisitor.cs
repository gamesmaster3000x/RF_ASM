﻿using Antlr4.Runtime.Misc;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar;
using Crimson.CSharp.Grammar.Statements;
using Crimson.CSharp.Grammar.Tokens;
using static Crimson.CSharp.Grammar.Tokens.Comparator;

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
                FullNameCToken id = VisitFullName(importCtx.fullName());
                ImportCStatement import = new ImportCStatement(path, id);
                compilation.AddImport(import);
            }

            // Add operation handlers
            IList<CrimsonParser.OperationHandlerContext> operationHandlersCtxs = context._opHandlers;
            foreach (CrimsonParser.OperationHandlerContext unitStatementCtx in operationHandlersCtxs)
            {
                OperationHandlerCStatement unitStatement = VisitOperationHandler(unitStatementCtx);
                compilation.AddOpHandler(unitStatement);
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

        public override OperationHandlerCStatement VisitOperationHandler([NotNull] CrimsonParser.OperationHandlerContext context)
        {
            CrimsonTypeCToken type1 = VisitType(context.t1);
            OperationResolvableValueCToken.OperationType opType = OperationResolvableValueCToken.ParseOpType(context.op.Text);
            CrimsonTypeCToken type2 = VisitType(context.t2);
            FullNameCToken id = VisitFullName(context.identifier);
            OperationHandlerCStatement ohsc = new OperationHandlerCStatement(type1, opType, type2, id);

            return ohsc;
        }

        private GlobalCStatement ParseGlobalStatement(CrimsonParser.GlobalStatementContext context)
        {
            if (context is CrimsonParser.GlobalVariableUnitStatementContext gvCtx)
            {
                CrimsonParser.GlobalVariableDeclarationContext declaration = gvCtx.globalVariableDeclaration();
                return VisitGlobalVariableDeclaration(declaration);
            }
            else if (context is CrimsonParser.FunctionUnitStatementContext fnCtx)
            {
                CrimsonParser.FunctionDeclarationContext declaration = fnCtx.functionDeclaration();
                return VisitFunctionDeclaration(declaration);
            }
            else if (context is CrimsonParser.StructureUnitStatementContext structureCtx)
            {
                CrimsonParser.StructureDeclarationContext declaration = structureCtx.structureDeclaration();
                return VisitStructureDeclaration(declaration);
            }
            else
            {
                throw new StatementParseException("The given CrimsonParser.CompilationUnitStatementContext " + context + " is not of a permissable type");
            }
        }

        public override GlobalVariableCStatement VisitGlobalVariableDeclaration([NotNull] CrimsonParser.GlobalVariableDeclarationContext context)
        {
            CrimsonParser.InternalVariableDeclarationContext ivdc = context.internalVariableDeclaration();
            CrimsonTypeCToken type = VisitType(ivdc.type());
            FullNameCToken identifier = VisitFullName(ivdc.fullName());
            SimpleValueCToken? simple;
            ComplexValueCToken? complex;
            if (ivdc.simple != null) return new GlobalVariableCStatement(type, identifier, VisitSimpleValue(ivdc.simple));
            else if (ivdc.complex != null) return new GlobalVariableCStatement(type, identifier, VisitComplexValue(ivdc.complex));
            else throw new CrimsonParserException("Cannot parse GlobalVariableDeclarationContext with value " + ivdc.GetText());
        }

        public override FunctionCStatement VisitFunctionDeclaration([NotNull] CrimsonParser.FunctionDeclarationContext context)
        {
            CrimsonTypeCToken returnType = VisitType(context.returnType);
            FunctionCStatement.Header header = VisitFunctionHeader(context.header);
            IList<InternalStatement> statements = VisitFunctionBody(context.body);
            return new FunctionCStatement(returnType, header, statements);

        }

        public override FunctionCStatement.Header VisitFunctionHeader([NotNull] CrimsonParser.FunctionHeaderContext context)
        {
            FullNameCToken identifier = VisitFullName(context.name);
            List<FunctionCStatement.Parameter> parameters = VisitParameterList(context.parameters);
            return new FunctionCStatement.Header(identifier, parameters);
        }

        public override StructureCStatement VisitStructureDeclaration([NotNull] CrimsonParser.StructureDeclarationContext context)
        {
            FullNameCToken identifier = VisitFullName(context.name);
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

        public VariableAssignmentCStatement ParseAssignVariable([NotNull] CrimsonParser.AssignVariableContext context)
        {
            if (context == null) throw new StatementParseException("Illegal null CrimsonParser.AssignVariableContext");
            if (context is CrimsonParser.AssignVariableDirectContext avdc)
            {
                return VisitAssignVariableDirect(avdc);
            }
            else if (context is CrimsonParser.AssignVariableAtPointerContext avapc)
            {
                return VisitAssignVariableAtPointer(avapc);
            }
            else
            {
                throw new StatementParseException("The given CrimsonParser.AssignVariableContext (" + context + ") is not of a permissable type");
            }
        }

        public override CrimsonTypeCToken VisitType([NotNull] CrimsonParser.TypeContext context)
        {
            FullNameCToken name = new FullNameCToken(context.GetText());
            return new CrimsonTypeCToken(name);
        }

        public override List<FunctionCStatement.Parameter> VisitParameterList([NotNull] CrimsonParser.ParameterListContext context)
        {
            List<FunctionCStatement.Parameter> parameters = new List<FunctionCStatement.Parameter>();
            foreach (CrimsonParser.ParameterContext paCxt in context.parameter())
            {
                CrimsonTypeCToken type = VisitType(paCxt.type());
                FullNameCToken identifier = VisitFullName(paCxt.name);
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
                return ParseAssignVariable(asvCtx);
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
            else if (stCtx is CrimsonParser.FunctionWhileStatementContext)
            {
                CrimsonParser.FunctionWhileStatementContext context = (CrimsonParser.FunctionWhileStatementContext)stCtx;
                CrimsonParser.WhileBlockContext whileCtx = context.whileBlock();
                return VisitWhileBlock(whileCtx);
            }
            else if (stCtx is CrimsonParser.FunctionAssemblyCallStatementContext context)
            {
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
            FullNameCToken identifier = VisitFullName(context.fullName());
            SimpleValueCToken? simple;
            ComplexValueCToken? complex;
            if (context.simple != null) return new InternalVariableCStatement(type, identifier, VisitSimpleValue(context.simple));
            else if (context.complex != null) return new InternalVariableCStatement(type, identifier, VisitComplexValue(context.complex));
            else throw new CrimsonParserException("Cannot parse InternalVariableDeclarationContext with value " + context.GetText());
        }

        public override FunctionCallCStatement VisitFunctionCall([NotNull] CrimsonParser.FunctionCallContext context)
        {
            FullNameCToken identifier = VisitFullName(context.name);
            IList<SimpleValueCToken> arguments = VisitArguments(context.arguments());
            FunctionCallCStatement call = new FunctionCallCStatement(identifier, arguments);
            return call;
        }

        public override IfBlockCStatement VisitIfBlock([NotNull] CrimsonParser.IfBlockContext context)
        {
            ConditionCToken condition = VisitCondition(context.condition());
            IList<InternalStatement> body = VisitFunctionBody(context.functionBody());
            CrimsonParser.ElseIfBlockContext eibCtx = context.elseIfBlock();
            CrimsonParser.ElseBlockContext elbCtx = context.elseBlock();
            ElseIfBlockCToken? elifBlock = eibCtx == null ? null : VisitElseIfBlock(eibCtx);
            ElseBlockCToken? elseBlock = elbCtx == null ? null : VisitElseBlock(elbCtx);
            IfBlockCStatement ifBlock = new IfBlockCStatement(condition, body, elifBlock, elseBlock);
            return ifBlock;
        }

        public override WhileBlockCStatement VisitWhileBlock([NotNull] CrimsonParser.WhileBlockContext context)
        {
            ConditionCToken condition = VisitCondition(context.condition());
            IList<InternalStatement> body = VisitFunctionBody(context.functionBody());
            WhileBlockCStatement ifBlock = new WhileBlockCStatement(condition, body);
            return ifBlock;
        }

        /*public override VariableAssignmentCStatement VisitAssignVariable([NotNull] CrimsonParser.AssignVariableContext context)
        {
            string identifier = context.Identifier().GetText();
            bool ptr = context.pointer != null;
            ResolvableValueCToken value = ParseResolvableValue(context.resolvableValue());
            VariableAssignmentCStatement assignment = new VariableAssignmentCStatement(identifier, value);
            return assignment;
        }*/

        public override VariableAssignmentCStatement VisitAssignVariableDirect([NotNull] CrimsonParser.AssignVariableDirectContext context)
        {
            FullNameCToken identifier = VisitFullName(context.name);
            if (context.simple != null) return new VariableAssignmentCStatement(identifier, VisitSimpleValue(context.simple));
            else if (context.complex != null) return new VariableAssignmentCStatement(identifier, VisitComplexValue(context.complex));
            else throw new CrimsonParserException($"Cannot assign no value to variable {identifier}");
        }

        public override VariableAssignmentCStatement VisitAssignVariableAtPointer([NotNull] CrimsonParser.AssignVariableAtPointerContext context)
        {
            //TODO AssignVariableAtPointer just adds an asterisk to the variable name
            FullNameCToken identifier = VisitFullName(context.fullName());
            if (context.simple != null) return new VariableAssignmentCStatement(identifier, VisitSimpleValue(context.simple));
            else if (context.complex != null) return new VariableAssignmentCStatement(identifier, VisitComplexValue(context.complex));
            else throw new CrimsonParserException($"Cannot assign no value to variable {identifier}");
        }

        public override ReturnCStatement VisitFunctionReturn([NotNull] CrimsonParser.FunctionReturnContext context)
        {
            CrimsonParser.SimpleValueContext? rvc = context?.simpleValue();
            SimpleValueCToken value = rvc == null ? new RawResolvableValueCToken("NULL") : VisitSimpleValue(rvc);
            ReturnCStatement ret = new ReturnCStatement(value);
            return ret;
        }

        public override ComplexValueCToken VisitComplexValue([NotNull] CrimsonParser.ComplexValueContext context)
        {
            if (context.op != null) return VisitOperation(context.op);
            else if (context.func != null) return new FunctionCallResolvableValueCToken(VisitFunctionCall(context.func));
            throw new CrimsonParserException("Cannot parse ComplexValueContext " + context.GetText());
        }

        public override SimpleValueCToken VisitSimpleValue([NotNull] CrimsonParser.SimpleValueContext context)
        {
            FullNameCToken identifier = VisitFullName(context.fullName());
            if (context.id != null) return new IdentifierSimpleValueCToken(VisitFullName(context.fullName()));
            else if (context.raw != null) return VisitRawValue(context.raw);
            throw new CrimsonParserException("Cannot parse SimpleValueContext " + context.GetText());
        }

        public override RawResolvableValueCToken VisitRawValue([NotNull] CrimsonParser.RawValueContext context)
        {
            return new RawResolvableValueCToken(context.GetText());
        }

        public override OperationResolvableValueCToken VisitOperation([NotNull] CrimsonParser.OperationContext context)
        {
            SimpleValueCToken leftToken = VisitSimpleValue(context.leftValue);
            OperationResolvableValueCToken.OperationType t = OperationResolvableValueCToken.ParseOpType(context.@operator.Text);
            SimpleValueCToken rightToken = VisitSimpleValue(context.rightValue);

            OperationResolvableValueCToken oct = new OperationResolvableValueCToken(leftToken, t, rightToken);
            return oct;
            // return new ResolvableValueCToken(context.GetText(), ResolvableValueCToken.ValueType.OPERATION);
        }

        public override AssemblyCallCStatement VisitAssemblyCall([NotNull] CrimsonParser.AssemblyCallContext context)
        {
            string assemblyText = context.assemblyText.Text;
            AssemblyCallCStatement call = new AssemblyCallCStatement(assemblyText);
            return call;
        }

/*        public  MemoryAllocationCStatement VisitAllocateMemory([NotNull] CrimsonParser.AllocateMemoryContext context)
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
*/
        public override IList<SimpleValueCToken> VisitArguments([NotNull] CrimsonParser.ArgumentsContext context)
        {
            IList<SimpleValueCToken> arguments = new List<SimpleValueCToken>();
            foreach (CrimsonParser.SimpleValueContext rvlCxt in context.simpleValue())
            {
                SimpleValueCToken value = VisitSimpleValue(rvlCxt);
                arguments.Add(value);
            }
            return arguments;
        }

        public override ConditionCToken VisitCondition([NotNull] CrimsonParser.ConditionContext context)
        {
            OperationResolvableValueCToken operation = VisitOperation(context.op);
            ConditionCToken condition = new ConditionCToken(operation);
            return condition;
        }

        public override ElseIfBlockCToken VisitElseIfBlock([NotNull] CrimsonParser.ElseIfBlockContext context)
        {
            IfBlockCStatement ifBlock = VisitIfBlock(context.ifBlock());
            ElseIfBlockCToken elseIfBlock = new ElseIfBlockCToken(ifBlock);
            return elseIfBlock;
        }

        public override ElseBlockCToken VisitElseBlock([NotNull] CrimsonParser.ElseBlockContext context)
        {
            IList<InternalStatement> statements = VisitFunctionBody(context.functionBody());
            ElseBlockCToken elseBlock = new ElseBlockCToken(statements);
            return elseBlock;
        }

        public override FullNameCToken VisitFullName ([NotNull] CrimsonParser.FullNameContext context)
        {
            return new FullNameCToken(context.libraryName.Text, context.memberName.Text);
        }
    }
}
