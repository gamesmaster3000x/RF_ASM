using Crimson.CSharp.Core;
using Crimson.CSharp.Parsing.Tokens;

namespace Crimson.CSharp.Parsing.Statements
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface INamed
    {
        public abstract FullNameCToken GetName ();
        public abstract void SetName (FullNameCToken name);
    }
}