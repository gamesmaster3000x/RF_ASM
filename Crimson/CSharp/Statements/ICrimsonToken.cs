using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public interface ICrimsonToken
    {
        public void Link(LinkingContext ctx);
    }
}