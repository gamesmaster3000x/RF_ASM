namespace Compiler.Parsing.Syntax.Functions
{
    internal class Parameter
    {
        public Parameter (string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
        string Name { get; set; }
    }
}