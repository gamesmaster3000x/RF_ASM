using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Crimson.ANTLR.Crimson;
using Crimson.CSharp.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Crimson.ANTLR.Crimson.CrimsonParser;

namespace Crimson.CSharp.Core
{
    internal class CrimsonProgramVisitor: CrimsonBaseVisitor<object>
    {
        public override object VisitCompilationUnit([NotNull] CrimsonParser.CompilationUnitContext context)
        {
            CompilationUnit compilation = new CompilationUnit();

            List<Package> packageDefinitions = (List<Package>) VisitPackageDefinitionList(context.packageDefinitions);

            foreach(Package p in packageDefinitions)
            {
                compilation.packages[p.QualifiedName] = p;
            }
            return compilation;
        }

        public override object VisitPackageDefinitionList([NotNull] CrimsonParser.PackageDefinitionListContext context)
        {
            Dictionary<string, Package> packages = new Dictionary<string, Package>();

            foreach(PackageDefinitionContext def in context._definitions)
            {
                Package p = (Package) VisitPackageDefinition(def);
                packages[p.QualifiedName] = p;
            }

            return packages;
        }

        public override object VisitPackageDefinition([NotNull] CrimsonParser.PackageDefinitionContext context)
        {
            Package p = new Package();

            p.PackageName = context.name.Text;
            p.Dependencies = (Dictionary<string, Dependency>) VisitPackageDependencyList(context.dependencies);
            p.Structures = new Dictionary<string, Structure>();
            p.Globals = new Dictionary<string, GlobalVariable>();
            p.Functions = new Dictionary<string, Function>();

            List<object> children = (List<object>) VisitChildren(context);

            foreach (object o in children)
            {
                if(o is Structure)
                {
                    Structure structure = (Structure) o;
                    p.Structures[structure.Name] = structure;
                } 
                else if (o is GlobalVariable)
                {
                    GlobalVariable globalVariable = (GlobalVariable) o;
                    p.Globals[globalVariable.Name] = globalVariable;
                }
                else if (o is Function)
                {
                    Function function = (Function) o;
                    p.Functions[function.Name] = function;
                } else
                {
                    throw new Exception("Wait what? This object isn't a GlobalVariable, Structure, or Function - it's " + (o == null ? "null" : " a " + o.GetType()));
                }
            }

            return p;
        }

        public override object VisitPackageBody([NotNull] PackageBodyContext context)
        {
            List<TopLevelStatementContext> statements = new List<TopLevelStatementContext>(context._topLevelStatements);
            return statements;
        }
        //TODO VisitGlobalVariableDeclaration
        public override object VisitGlobalVariableDeclaration([NotNull] GlobalVariableDeclarationContext context)
        {
            return base.VisitGlobalVariableDeclaration(context);
        }

        //TODO VisitStructureDeclaration
        public override object VisitStructureDeclaration([NotNull] StructureDeclarationContext context)
        {
            return base.VisitStructureDeclaration(context);
        }

        //TODO VisitFunctionDeclaration
        public override object VisitFunctionDeclaration([NotNull] FunctionDeclarationContext context)
        {
            return base.VisitFunctionDeclaration(context);
        }
    }
}
