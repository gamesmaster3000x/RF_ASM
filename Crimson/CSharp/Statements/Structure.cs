namespace Crimson.CSharp.Reflection
{
    internal class Structure : GlobalStatement
    {
        public Structure(string identifier, IList<InternalStatement> body)
        {
            Identifier = identifier;
            Body = body;
        }

        public string Identifier { get; }
        public IList<InternalStatement> Body { get; }
    }
}