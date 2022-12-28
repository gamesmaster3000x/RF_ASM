using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class ElseBlock : ICrimsonToken
    {
        public ElseBlock(IList<InternalStatement> statements)
        {
            Statements = statements;
        }

        public IList<InternalStatement> Statements { get; }

        public void Link(LinkingContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}