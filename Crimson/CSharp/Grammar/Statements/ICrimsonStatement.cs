using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Statements
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface ICrimsonStatement
    {
        public abstract void Link(LinkingContext ctx);

        public abstract bool IsLinked();

        public abstract Fragment GetCrimsonBasic();
    }
}