using Crimson.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Statements
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    public class GlobalVariable : GlobalStatement
    {
        public GlobalVariable(InternalVariable intern)
        {
            Intern = intern;
        }

        public InternalVariable Intern { get; }

        public override void Link(LinkingContext ctx)
        {
            Intern.Link(ctx);
        }
    }
}
