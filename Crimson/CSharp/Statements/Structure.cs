namespace Crimson.CSharp.Reflection
{
    internal class Structure: GlobalStatement
    {
        public Structure(string text, IList<InternalStatement> body)
        {
            Body = body;
        }

        public string Name { get; set; }
        public IList<InternalStatement> Body { get; }
    }
}