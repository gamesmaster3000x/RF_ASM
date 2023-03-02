using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Statements
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface INamedStatement: ICrimsonStatement
    {
        public abstract FullNameCToken GetName ();
        public abstract void SetName (FullNameCToken name);
    }
}