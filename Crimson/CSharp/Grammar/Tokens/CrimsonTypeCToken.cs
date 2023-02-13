using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Text.RegularExpressions;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class CrimsonTypeCToken : ICrimsonToken
    {
        private static readonly Regex WHITESPACE = new Regex(@"\s+");

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
            if (Text.Equals("int"))
            {
                return 4;
            }
            if (Text.Equals("byte"))
            {
                return 1;
            }
            return 666;
        }
    }
}