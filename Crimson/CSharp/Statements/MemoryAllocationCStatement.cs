using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class MemoryAllocationCStatement : InternalStatement
    {
        private string identifier;
        private int number;

        public MemoryAllocationCStatement(string identifier, int number)
        {
            this.identifier = identifier;
            this.number = number;
        }

        public override void Link(LinkingContext ctx)
        {
            return;
        }

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            List<BasicStatement> crimsonBasic = new List<BasicStatement>();

            crimsonBasic.Add(new MemoryAllocateBStatement(identifier, number));

            return crimsonBasic;
        }
    }
}