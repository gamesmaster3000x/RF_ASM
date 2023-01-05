using Crimson.CSharp.Core;
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

        public IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>() { new TestBStatement($"Type:{Text}") };
        }
    }
}