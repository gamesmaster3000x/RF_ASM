using Crimson.CSharp.Core;
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

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>() { new AssemblyBStatement(assemblyText) };
        }
    }
}