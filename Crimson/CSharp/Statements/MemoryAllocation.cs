using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class MemoryAllocation : InternalStatement
    {
        private string identifier;
        private int number;

        public MemoryAllocation(string identifier, int number)
        {
            this.identifier = identifier;
            this.number = number;
        }

        public override void Link(LinkingContext ctx)
        {
            return;
        }
    }
}