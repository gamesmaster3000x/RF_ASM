using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public class CrimsonTypeCToken : ICrimsonToken
    {
        public string Text { get; set; }

        public CrimsonTypeCToken(string text)
        {
            Text = text;
        }

        public void Link(LinkingContext ctx)
        {
            Text = LinkerHelper.LinkIdentifier(Text, ctx);
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement($"Type:{Text}"));
            return f;
        }

        public override string ToString()
        {
            return Text;
        }

        internal int GetByteSize()
        {
            return 666;
        }
    }
}