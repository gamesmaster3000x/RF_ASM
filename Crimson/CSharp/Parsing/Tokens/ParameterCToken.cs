namespace Crimson.CSharp.Parsing.Tokens
{
    internal class ParameterCToken
    {
        public ParameterCToken (string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
        TypeCToken Type { get; set; }
        string Name { get; set; }
    }
}