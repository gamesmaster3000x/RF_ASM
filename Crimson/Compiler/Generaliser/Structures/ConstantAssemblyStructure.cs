using Compiler.Generaliser;

namespace Compiler.Generaliser.Structures
{
    /// <summary>
    /// A constant value in the assembly, for example:
    /// <code>(RFASM) .WORD data</code>
    /// The clue is in the name: this value DOES NOT CHANGE. It is resolved HERE AND NOW - NO DEFERRING - so it needs to be VERY SIMPLE AND SMALL.
    /// </summary>
    public class ConstantAssemblyStructure : IGeneralAssemblyStructure
    {
        public GeneralisationContext Context { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ConstantAssemblyStructure (GeneralisationContext context, string name, string value)
        {
            Context = context;
            Name = name;
            Value = value;
        }

        IEnumerable<IGeneralAssemblyStructure> IGeneralAssemblyStructure.GetSubStructures ()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }

        public override string ToString ()
        {
            return $"CON: {Name}={Value}";
        }
    }
}