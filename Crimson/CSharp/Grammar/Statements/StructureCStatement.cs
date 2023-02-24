using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Statements
{
    public class StructureCStatement : GlobalCStatement
    {
        public StructureCStatement(FullNameCToken name, IList<InternalStatement> body)
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