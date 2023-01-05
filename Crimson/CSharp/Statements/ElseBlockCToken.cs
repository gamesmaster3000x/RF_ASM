using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class ElseBlockCToken : ICrimsonToken
    {
        public ElseBlockCToken(IList<InternalStatement> statements)
        {
            Statements = statements;
        }

        public IList<InternalStatement> Statements { get; }

        public IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>();
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}