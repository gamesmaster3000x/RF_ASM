using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class AssemblyCallCStatement : ICrimsonStatement
    {
        private string assemblyText;

        public AssemblyCallCStatement(string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new AssemblyBStatement(assemblyText));
            return f;
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }

        public void Link(LinkingContext ctx)
        {

        }
    }
}