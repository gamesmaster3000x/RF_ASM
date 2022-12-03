using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Crimson.AntlrBuild;
using Crimson.CSharp.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    internal class CrimsonProgramVisitor: CrimsonBaseVisitor<object>
    {
        public override CompilationUnit VisitCompilationUnit([NotNull] CrimsonParser.CompilationUnitContext context)
        {
            CompilationUnit compilation = new CompilationUnit();

            // Get all available context fields
            //PackageDefinitionListContext packageDefinitionListContext = context.packageDefinitions;

            Console.WriteLine("COMPILATION UNIT");
            foreach (CrimsonParser.PackageDefinitionContext c in context.packageDefinitions._definitions)
            {
                Console.WriteLine(c);
            }
            
            // Populate output fields
            //Dictionary<string, Package> packageDefinitions = VisitPackageDefinitionList(packageDefinitionListContext);
            //compilation.packages = packageDefinitions;

            return compilation;
        }

        public override Dictionary<string, Package> VisitPackageDefinitionList([NotNull] CrimsonParser.PackageDefinitionListContext packageDefinitionListContext)
        {
            Dictionary<string, Package> packages = new Dictionary<string, Package>();

            // No need to get context._packageDefinition (part of _definitions)
            IList<CrimsonParser.PackageDefinitionContext> packageDefinitionContexts = packageDefinitionListContext._definitions;

            foreach(CrimsonParser.PackageDefinitionContext packageDefinitionContext in packageDefinitionContexts)
            {
                Package p = VisitPackageDefinition(packageDefinitionContext);
                packages[p.QualifiedName] = p;
            }

            return packages;
        }

        public override Package VisitPackageDefinition([NotNull] CrimsonParser.PackageDefinitionContext context)
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
    }
}
