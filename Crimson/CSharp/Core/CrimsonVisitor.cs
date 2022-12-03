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
            
            // Populate output fields
            //Dictionary<string, Package> packageDefinitions = VisitPackageDefinitionList(packageDefinitionListContext);
            //compilation.packages = packageDefinitions;

            return compilation;
        }
    }
}
