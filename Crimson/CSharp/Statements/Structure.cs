namespace Crimson.CSharp.Reflection
{
    internal class Structure: GlobalStatement
    {
        public Structure(string text) : base(text)
        {
        }

        public string Name { get; set; }
    }
}