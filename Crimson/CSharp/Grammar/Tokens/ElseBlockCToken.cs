using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Grammar.Tokens
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
            Fragment fragment = new Fragment(0);
            fragment.Add(new CommentBStatement(""));

            Fragment elseHead = new Fragment(0);
            elseHead.Add(new LabelBStatement("ELSE"));

            Fragment elseBody = new Fragment(1);
            foreach (var s in Statements)
            {
                elseBody.Add(s.GetCrimsonBasic());
            }

            fragment.Add(elseHead);
            fragment.Add(elseBody);

            return fragment;
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}