using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Reflection
{
    internal class Parameter
    {
        public Parameter(CrimsonType type, string identifier)
        {
            Type1 = type;
            Identifier = identifier;
        }

        public CrimsonType Type1 { get; }
        public string Identifier { get; }
        Type Type { get; set; }
        string Name { get; set; }
    }
}