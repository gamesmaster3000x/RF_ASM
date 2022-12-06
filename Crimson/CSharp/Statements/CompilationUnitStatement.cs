namespace Crimson.CSharp.Reflection
{
    internal class CompilationUnitStatement
    {
        public string RawText { get; }

        public CompilationUnitStatement(string rawText)
        {
            RawText = rawText;
        }
    }
}