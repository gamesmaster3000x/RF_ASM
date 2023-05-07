namespace Compiler.Parsing.Syntax
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface INamed
    {
        public abstract FullName GetName();
        public abstract void SetName(FullName name);
    }
}