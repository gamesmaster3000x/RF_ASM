using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Tokens
{
    internal class ElseBlockCToken : ICrimsonToken
    {
        public ElseBlockCToken(Scope statements)
        {
            Scope = statements;
        }

        public Scope Scope { get; }

        public Fragment GetCrimsonBasic()
        {
            Fragment fragment = new Fragment(0);
            fragment.Add(new CommentBStatement(""));

            Fragment elseHead = new Fragment(0);
            elseHead.Add(new LabelBStatement("ELSE"));

            Fragment elseBody = new Fragment(1);
            elseBody.Add(Scope.GetCrimsonBasic());

            fragment.Add(elseHead);
            fragment.Add(elseBody);

            return fragment;
        }

        public void Link(LinkingContext ctx)
        {
            Scope.Link(ctx);
            return;
        }
    }
}