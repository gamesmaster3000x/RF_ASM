using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public interface ICrimsonToken
    {
        public void Link(LinkingContext ctx);
        public Fragment GetCrimsonBasic();
    }
}