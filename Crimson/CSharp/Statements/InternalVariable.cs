namespace Crimson.CSharp.Reflection
{
    internal class InternalVariable: InternalStatement
    {
        private CrimsonType type;
        private string identifier;

        public InternalVariable(CrimsonType type, string identifier)
        {
            this.type = type;
            this.identifier = identifier;
        }

        public InternalVariable(CrimsonType type, string identifier, ResolvableValue? value) : this(type, identifier)
        {
            Value = value;
        }

        public ResolvableValue? Value { get; }
    }
}