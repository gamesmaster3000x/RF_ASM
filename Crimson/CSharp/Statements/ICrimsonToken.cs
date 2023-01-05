using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public interface ICrimsonToken
    {
        public void Link(LinkingContext ctx);
        public IList<BasicStatement> GetCrimsonBasic();
    }
}