using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class CrimsonType : ICrimsonToken
    {
        public string Text { get; set; }

        public CrimsonType(string text)
        {
            Text = text;
        }

        public void Link(LinkingContext ctx)
        {
            Text = LinkerHelper.LinkIdentifier(Text, ctx);
        }

        public IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>();
        }
    }
}