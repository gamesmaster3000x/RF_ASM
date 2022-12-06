using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class AssemblyCall : InternalStatement
    {
        private string assemblyText;

        public AssemblyCall(string assemblyText)
        {
            this.assemblyText = assemblyText;
        }
    }
}