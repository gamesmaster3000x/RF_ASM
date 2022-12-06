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
                GlobalStatement unitStatement = ParseCompilationUnitStatement(unitStatementCtx);
                compilation.AddStatement(unitStatement);
            }

            // Populate output fields
            //Dictionary<string, Package> packageDefinitions = VisitPackageDefinitionList(packageDefinitionListContext);
            //compilation.packages = packageDefinitions;

            return compilation;
        }

        private GlobalStatement ParseCompilationUnitStatement(CrimsonParser.GlobalStatementContext context)
        {
            if (context is CrimsonParser.GlobalVariableUnitStatementContext)
            {
                CrimsonParser.GlobalVariableUnitStatementContext gvCtx = (CrimsonParser.GlobalVariableUnitStatementContext)context;
                CrimsonParser.InternalVariableDeclarationContext declaration = gvCtx.globalVariableDeclaration().declaration;
                string identifier = declaration.Identifier().GetText();
                ResolvableValue value = VisitResolvableValue(declaration.resolvableValue());
                return new GlobalVariable(gvCtx.GetText(), identifier, value);
            } 
            else if (context is CrimsonParser.FunctionUnitStatementContext)
            {
                CrimsonParser.FunctionUnitStatementContext fnCtx = (CrimsonParser.FunctionUnitStatementContext)context;
                CrimsonParser.FunctionDeclarationContext declaration = fnCtx.functionDeclaration();
                string name = declaration.name.Text;
                CrimsonType returnType = VisitType(declaration.returnType);
                IList<InternalStatement> statements = VisitFunctionBody(declaration.body);
                IList<Function.Parameter> parameters = VisitParameterList(declaration.parameters);
                return new Function(returnType, name, parameters, statements);
            } 
            else if (context is CrimsonParser.StructureUnitStatementContext)
            {
                return new Structure(context.GetText());
            } else
            {
                throw new StatementParseException("The given CrimsonParser.CompilationUnitStatementContext " + context + " is not of a permissable type");
            }
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
                InternalStatement statement = ParseFunctionStatement(stCtx);
                statements.Add(statement);
            }
            return statements;
        }

        private InternalStatement ParseFunctionStatement(CrimsonParser.InternalStatementContext stCtx)
        {
            if(stCtx is CrimsonParser.FunctionVariableDeclarationStatementContext)
            {
                CrimsonParser.FunctionVariableDeclarationStatementContext context = (CrimsonParser.FunctionVariableDeclarationStatementContext)stCtx;
                CrimsonParser.InternalVariableDeclarationContext ivdCtx = context.internalVariableDeclaration();
                CrimsonType type = VisitType(ivdCtx.type());
                string identifier = ivdCtx.Identifier().GetText();
                ResolvableValue? value = ivdCtx.resolvableValue() == null ? null : VisitResolvableValue(ivdCtx.resolvableValue());
                InternalVariable variable = new InternalVariable(type, identifier, value);
                return variable;
            }
            else if (stCtx is CrimsonParser.FunctionReturnStatementContext)
            {
                CrimsonParser.FunctionReturnStatementContext context = (CrimsonParser.FunctionReturnStatementContext)stCtx;
                CrimsonParser.FunctionReturnContext rtnCtx = context.functionReturn();
                ResolvableValue value = VisitResolvableValue(rtnCtx.resolvableValue());
                return new Return(value);
            }
            else if (stCtx is CrimsonParser.FunctionAssignVariableStatementContext)
            {
                CrimsonParser.FunctionAssignVariableStatementContext context = (CrimsonParser.FunctionAssignVariableStatementContext)stCtx;
                CrimsonParser.AssignVariableContext asvCtx = context.assignVariable();
                string identifier = asvCtx.Identifier().GetText();
                ResolvableValue value = VisitResolvableValue(asvCtx.resolvableValue());
                VariableAssignment assignment = new VariableAssignment(identifier, value);
                return assignment;
            }
            else if (stCtx is CrimsonParser.FunctionAllocateMemoryStatementContext)
            {
                CrimsonParser.FunctionAllocateMemoryStatementContext context = (CrimsonParser.FunctionAllocateMemoryStatementContext)stCtx;
                CrimsonParser.AllocateMemoryContext almCtx = context.allocateMemory();
                string identifier = almCtx.Identifier().GetText();
                string numberText = almCtx.Number().GetText();
                int number;
                try
                {
                    number = Int32.Parse(numberText);
                } catch (FormatException f)
                {
                    throw new StatementParseException("Failed to parse string->int " + numberText + " while allocating memory for " + identifier, f);
                }
                return new MemoryAllocation(identifier, number);
            }
            else if (stCtx is CrimsonParser.FunctionFunctionCallStatementContext)
            {
                CrimsonParser.FunctionFunctionCallStatementContext context = (CrimsonParser.FunctionFunctionCallStatementContext)stCtx;
                CrimsonParser.FunctionCallContext fncCtx = context.functionCall();
                string identifier = fncCtx.Identifier().GetText();
                IList<FunctionArgument> arguments = VisitInputParameters(fncCtx.inputParameters());
                FunctionCall call = new FunctionCall(identifier, arguments);
                return call;
            }
            else if (stCtx is CrimsonParser.FunctionIfStatementContext)
            {
                CrimsonParser.FunctionIfStatementContext context = (CrimsonParser.FunctionIfStatementContext)stCtx;
                CrimsonParser.IfBlockContext ifCtx = context.ifBlock();
                Condition condition = VisitCondition(ifCtx.condition());
                IList<InternalStatement> body = VisitFunctionBody(ifCtx.functionBody());
                ElifBlock elifBlock = VisitElifBlock(ifCtx.elifBlock());
                ElseBlock elseBlock = VisitElseBlock(ifCtx.elseBlock());
                IfBlock ifBlock = new IfBlock(condition, body, elifBlock, elseBlock);
                return ifBlock;
            }
            else if (stCtx is CrimsonParser.FunctionAssemblyCallStatementContext)
            {
                CrimsonParser.FunctionAssemblyCallStatementContext context = (CrimsonParser.FunctionAssemblyCallStatementContext)stCtx;
                CrimsonParser.AssemblyCallContext acCtx = context.assemblyCall();
                string assemblyText = acCtx.assemblyText.Text;
                AssemblyCall call = new AssemblyCall(assemblyText);
                return call;
            }
            else
            {
                throw new StatementParseException("The given CrimsonParser.FunctionStatementContext " + stCtx + " is not of a permissable type");
            }
        }

        public override IList<FunctionArgument> VisitInputParameters([NotNull] CrimsonParser.InputParametersContext context)
        {
            return null;
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
