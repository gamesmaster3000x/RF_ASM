using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class Structure : GlobalStatement
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