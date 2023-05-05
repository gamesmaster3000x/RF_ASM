using Compiler.Parsing.Tokens;

namespace Compiler.Parsing.Statements
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