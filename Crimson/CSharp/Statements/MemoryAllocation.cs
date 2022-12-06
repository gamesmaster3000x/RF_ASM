using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class MemoryAllocation : FunctionOnlyStatement
    {
        private string identifier;
        private int number;

        public MemoryAllocation(string identifier, int number)
        {
            this.identifier = identifier;
            this.number = number;
        }
    }
}