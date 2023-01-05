namespace Crimson.CSharp.Statements
{
    internal class ParameterCToken
    {
        public ParameterCToken(CrimsonTypeCToken type, string identifier)
        {
            Type1 = type;
            Identifier = identifier;
        }

        public CrimsonTypeCToken Type1 { get; }
        public string Identifier { get; }
        TypeCToken Type { get; set; }
        string Name { get; set; }
    }
}