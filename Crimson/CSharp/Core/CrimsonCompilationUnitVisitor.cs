using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Reflection;
using Crimson.CSharp.Statements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Crimson.CSharp.Core
{
    internal class CrimsonCompiliationUnitVisitor: CrimsonBaseVisitor<object>
    {
        public override CompilationUnit VisitCompilationUnit([NotNull] CrimsonParser.CompilationUnitContext context)
        {
            CompilationUnit compilation = new CompilationUnit();

            // Visit imports
            IList<CrimsonParser.ImportUnitContext> importCtxs = context._imports;
            foreach (CrimsonParser.ImportUnitContext importCtx in importCtxs)
            {
                string path = importCtx.path.Text;
                string id = importCtx.identifier.Text;
                Import import = new Import(path, id);
                compilation.AddImport(import);
            }

            // Visit Compilation-Unit statements
            IList<CrimsonParser.GlobalStatementContext> unitStatementCtxs = context._statements;
            foreach (CrimsonParser.GlobalStatementContext unitStatementCtx in unitStatementCtxs)
            {
                GlobalStatement unitStatement = ParseGlobalStatement(unitStatementCtx);
                compilation.AddStatement(unitStatement);
            }

            // Populate output fields
            //Dictionary<string, Package> packageDefinitions = VisitPackageDefinitionList(packageDefinitionListContext);
            //compilation.packages = packageDefinitions;

            return compilation;
        }

        // ----------------------------------------------------
        // -------------------------- GLOBAL STATEMENTS
        // ----------------------------------------------------

        private GlobalStatement ParseGlobalStatement(CrimsonParser.GlobalStatementContext context)
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

        public override GlobalVariable VisitGlobalVariableDeclaration([NotNull] CrimsonParser.GlobalVariableDeclarationContext context)
        {
            InternalVariable intern = VisitInternalVariableDeclaration(context.internalVariableDeclaration());
            GlobalVariable global = new GlobalVariable(intern);
            return global;
        }

        public override Function VisitFunctionDeclaration([NotNull] CrimsonParser.FunctionDeclarationContext context)
        {
            string name = context.name.Text;
            CrimsonType returnType = VisitType(context.returnType);
            IList<InternalStatement> statements = VisitFunctionBody(context.body);
            IList<Function.Parameter> parameters = VisitParameterList(context.parameters); 
            return new Function(returnType, name, parameters, statements);

        }

        public override Structure VisitStructureDeclaration([NotNull] CrimsonParser.StructureDeclarationContext context)
        {
            string identifier = context.Identifier().GetText();
            IList<InternalStatement> body = VisitStructureBody(context.structureBody());
            Structure structure = new Structure(identifier, body);
            return structure;
        }

        public override IList<InternalStatement> VisitStructureBody([NotNull] CrimsonParser.StructureBodyContext context)
        {
            IList<InternalStatement> statements = new List<InternalStatement>();
            foreach (CrimsonParser.InternalVariableDeclarationContext ivdCtx in context.internalVariableDeclaration())
            {
                InternalVariable var = VisitInternalVariableDeclaration(ivdCtx);
            }
            return statements;
        }

        public override ResolvableValue VisitResolvableValue([NotNull] CrimsonParser.ResolvableValueContext context)
        {
            return new ResolvableValue(null);
        }

        public override CrimsonType VisitType([NotNull] CrimsonParser.TypeContext context)
        {
            return new CrimsonType(context.GetText());
        }

        public override IList<Function.Parameter> VisitParameterList([NotNull] CrimsonParser.ParameterListContext context)
        {
            List<Function.Parameter> parameters = new List<Function.Parameter>();
            foreach (CrimsonParser.ParameterContext paCxt in context.parameter())
            {
                CrimsonType type = VisitType(paCxt.type());
                string identifier = paCxt.Identifier().GetText();
                Function.Parameter parameter = new Function.Parameter(type, identifier);
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

        public override InternalVariable VisitInternalVariableDeclaration([NotNull] CrimsonParser.InternalVariableDeclarationContext context)
        {
            CrimsonType type = VisitType(context.type());
            string identifier = context.Identifier().GetText();
            ResolvableValue? value = context.resolvableValue() == null ? null : VisitResolvableValue(context.resolvableValue());
            InternalVariable variable = new InternalVariable(type, identifier, value);
            return variable;
        }

        public override FunctionCall VisitFunctionCall([NotNull] CrimsonParser.FunctionCallContext context)
        {
            string identifier = context.Identifier().GetText();
            IList<ResolvableValue> arguments = VisitInputParameters(context.inputParameters());
            FunctionCall call = new FunctionCall(identifier, arguments);
            return call;
        }

        public override IfBlock VisitIfBlock([NotNull] CrimsonParser.IfBlockContext context)
        {
            Condition condition = VisitCondition(context.condition());
            IList<InternalStatement> body = VisitFunctionBody(context.functionBody());
            ElifBlock elifBlock = VisitElifBlock(context.elifBlock());
            ElseBlock elseBlock = VisitElseBlock(context.elseBlock());
            IfBlock ifBlock = new IfBlock(condition, body, elifBlock, elseBlock);
            return ifBlock;
        }

        public override VariableAssignment VisitAssignVariable([NotNull] CrimsonParser.AssignVariableContext context)
        {
            string identifier = context.Identifier().GetText();
            ResolvableValue value = VisitResolvableValue(context.resolvableValue());
            VariableAssignment assignment = new VariableAssignment(identifier, value);
            return assignment;
        }

        public override Return VisitFunctionReturn([NotNull] CrimsonParser.FunctionReturnContext context)
        {
            ResolvableValue value = VisitResolvableValue(context.resolvableValue());
            Return ret = new Return(value);
            return ret;
        }

        public override AssemblyCall VisitAssemblyCall([NotNull] CrimsonParser.AssemblyCallContext context)
        {
            string assemblyText = context.assemblyText.Text;
            AssemblyCall call = new AssemblyCall(assemblyText);
            return call;
        }

        public override MemoryAllocation VisitAllocateMemory([NotNull] CrimsonParser.AllocateMemoryContext context)
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
            MemoryAllocation allocation = new MemoryAllocation(identifier, number);
            return allocation;
        }

        public override IList<ResolvableValue> VisitInputParameters([NotNull] CrimsonParser.InputParametersContext context)
        {
            IList<ResolvableValue> arguments = new List<ResolvableValue>();
            foreach (CrimsonParser.ResolvableValueContext rvlCxt in context.resolvableValue())
            {
                ResolvableValue value = VisitResolvableValue(rvlCxt);
                arguments.Add(value);
            }
            return arguments;
        }

        public override Condition VisitCondition([NotNull] CrimsonParser.ConditionContext context)
        {
            return null;
        }

        public override ElifBlock VisitElifBlock([NotNull] CrimsonParser.ElifBlockContext context)
        {
            return null;
        }

        public override ElseBlock VisitElseBlock([NotNull] CrimsonParser.ElseBlockContext context)
        {
            return null;
        }
    }
}
