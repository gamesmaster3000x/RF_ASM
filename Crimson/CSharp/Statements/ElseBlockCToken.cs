using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
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

        public Fragment GetCrimsonBasic()
        {
            Fragment basicStatements = new Fragment(0);

            foreach (var s in Statements)
            {
                basicStatements.Add(s.GetCrimsonBasic());
            }
            
            return basicStatements;
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}