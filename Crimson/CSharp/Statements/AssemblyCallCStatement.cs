using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class AssemblyCallCStatement : InternalStatement
    {
        private string assemblyText;

        public AssemblyCallCStatement(string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public override void Link(LinkingContext ctx)
        {
            return;
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new AssemblyBStatement(assemblyText));
            return f;
        }
    }
}