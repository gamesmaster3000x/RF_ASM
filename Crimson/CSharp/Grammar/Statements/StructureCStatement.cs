using Crimson.CSharp.Core;

namespace Crimson.CSharp.Grammar.Statements
{
    public class StructureCStatement : GlobalCStatement
    {
        public StructureCStatement(string name, IList<InternalStatement> body)
        {
            Name = name;
            Body = body;
        }

        public IList<InternalStatement> Body { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }
    }
}