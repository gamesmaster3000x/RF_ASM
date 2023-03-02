using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class ScopeCToken : ICrimsonToken
    {
        public IList<InternalStatement> Statements { get; protected set; }
        public bool Linked { get; set; }

        public ScopeCToken (IList<InternalStatement> statements)
        {
            Statements = statements;
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new PushSfBStatement());
            foreach (var s in Statements)
            {
                f.Add(s.GetCrimsonBasic());
            }
            f.Add(new PopSfBStatement());
            return f;
        }

        public void Link (LinkingContext ctx)
        {
            foreach (var statement in Statements)
            {
                statement.Link (ctx);
            }
        }
    }
}