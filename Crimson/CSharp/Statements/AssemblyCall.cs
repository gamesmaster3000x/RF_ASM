using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class AssemblyCall : InternalStatement
    {
        private string assemblyText;

        public AssemblyCall(string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public override void Link(LinkingContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}