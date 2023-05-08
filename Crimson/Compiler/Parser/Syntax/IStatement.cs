namespace Compiler.Parser.Syntax
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface IStatement
    {
        string GetText ();
    }
}