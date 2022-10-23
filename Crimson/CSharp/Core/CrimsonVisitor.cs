using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Crimson.ANTLR.Crimson;
using Crimson.CSharp.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Crimson.ANTLR.Crimson.CrimsonParser;

namespace Crimson.CSharp.Core
{
    internal class CrimsonProgramVisitor: CrimsonBaseVisitor<Compilation>
    {
        public override Compilation VisitProgram([NotNull] CrimsonParser.ProgramContext context)
        {
            PackageDefinitionListContext list = context.packageDefinitions;
            IList<PackageDefinitionContext> definitions = list._definitions;

            // Extract metadata from package definition
            foreach (PackageDefinitionContext dCxt in definitions)
            {
                string name = dCxt.name.Text;
                PackageDependencyListContext dependencies = dCxt.dependencies;
                foreach(PackageDependencyContext dependency in dependencies._dependencies)
                {

                }
            }

            return VisitChildren(list);
        }
        public override Compilation VisitPackageDefinitionList([NotNull] CrimsonParser.PackageDefinitionListContext context)
        {
            foreach (ParserRuleContext definition in context.children)
            {

            }
            return base.VisitPackageDefinition(null);
        }

        public override Compilation VisitPackageDefinition([NotNull] CrimsonParser.PackageDefinitionContext context)
        {
            return base.VisitPackageDefinition(context);
        }
    }
}
