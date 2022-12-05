namespace Crimson.CSharp.Reflection
{
    internal class Structure: CompilationUnitStatement
    {
        public Structure(string text) : base(text)
        {
        }

        public string Name { get; set; }
    }
}