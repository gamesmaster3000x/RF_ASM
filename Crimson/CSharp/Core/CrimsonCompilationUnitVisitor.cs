using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Crimson.AntlrBuild;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Reflection;
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
            IList<CrimsonParser.CompilationUnitStatementContext> unitStatementCtxs = context._statements;
            foreach (CrimsonParser.CompilationUnitStatementContext unitStatementCtx in unitStatementCtxs)
            {
                CompilationUnitStatement unitStatement = ParseCompilationUnitStatement(unitStatementCtx);
                compilation.AddStatement(unitStatement);
            }

            // Populate output fields
            //Dictionary<string, Package> packageDefinitions = VisitPackageDefinitionList(packageDefinitionListContext);
            //compilation.packages = packageDefinitions;

            return compilation;
        }

        private CompilationUnitStatement ParseCompilationUnitStatement(CrimsonParser.CompilationUnitStatementContext context)
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
                IList<FunctionOnlyStatement> statements = VisitFunctionBody(declaration.body);
                IList<Parameter> parameters = VisitParameterList(declaration.parameters);
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

        public override IList<Parameter> VisitParameterList([NotNull] CrimsonParser.ParameterListContext context)
        {
            List<Parameter> parameters = new List<Parameter>();
            foreach (CrimsonParser.ParameterContext paCxt in context.parameter())
            {
                CrimsonType type = VisitType(paCxt.type());
                string identifier = paCxt.Identifier().GetText();
                Parameter parameter = new Parameter(type, identifier);
                parameters.Add(parameter);
            }
            return parameters;
        }

        public override IList<FunctionOnlyStatement> VisitFunctionBody([NotNull] CrimsonParser.FunctionBodyContext context)
        {
            List<FunctionOnlyStatement> statements = new List<FunctionOnlyStatement>();
            foreach (CrimsonParser.FunctionStatementContext stCtx in context._statements)
            {
                FunctionOnlyStatement statement = ParseFunctionStatement(stCtx);
                statements.Add(statement);
            }
            return statements;
        }

        private FunctionOnlyStatement ParseFunctionStatement(CrimsonParser.FunctionStatementContext stCtx)
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
            }
            else if (stCtx is CrimsonParser.FunctionAllocateMemoryStatementContext)
            {

            }
            else if (stCtx is CrimsonParser.FunctionFunctionCallStatementContext)
            {

            }
            else if (stCtx is CrimsonParser.FunctionIfStatementContext)
            {

            }
            else if (stCtx is CrimsonParser.FunctionAssemblyCallStatementContext)
            {

            }
            else
            {
                throw new StatementParseException("The given CrimsonParser.FunctionStatementContext " + stCtx + " is not of a permissable type");
            }
        }
    }
}
