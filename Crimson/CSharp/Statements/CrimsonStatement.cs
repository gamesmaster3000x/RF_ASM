using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public abstract class CrimsonStatement : ICrimsonToken
    {
        public abstract void Link(LinkingContext ctx);
    }
}