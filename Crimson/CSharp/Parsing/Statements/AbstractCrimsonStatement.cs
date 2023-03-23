using Crimson.CSharp.Assembly;
using Crimson.CSharp.Grammar.Tokens;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Statements
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public abstract class AbstractCrimsonStatement
    {
        public bool Linked { get; set; }

        public abstract void Link (LinkingContext ctx);

        public abstract Fragment GetCrimsonBasic ();
    }
}