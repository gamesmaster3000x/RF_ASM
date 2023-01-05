using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class Structure : GlobalCStatement
    {
        public Structure(string name, IList<InternalStatement> body)
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