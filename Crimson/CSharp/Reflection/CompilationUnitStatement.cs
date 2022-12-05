namespace Crimson.CSharp.Reflection
{
    internal class CompilationUnitStatement
    {
        public string RawText { get; }

        public CompilationUnitStatement(string text)
        {
            RawText = text;
        }
    }
}