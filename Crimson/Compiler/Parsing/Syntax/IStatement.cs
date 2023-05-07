namespace Compiler.Parsing.Syntax
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface IStatement
    {
        string GetText();
    }
}