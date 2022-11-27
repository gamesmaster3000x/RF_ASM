using Antlr4.Runtime.Misc;
using Crimson.ANTLR.Crimson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    internal class CrimsonListener: CrimsonBaseListener
    {
        public override void EnterCompilationUnit([NotNull] CrimsonParser.CompilationUnitContext context)
        {
            foreach (CrimsonParser.PackageDefinitionContext packageDefinitionContext in context.packageDefinitions._definitions)
            {
                Console.WriteLine(packageDefinitionContext);
            }
        }
    }
}
