using Compiler.Generalising;
using Compiler.Mapping;

namespace Compiler.Parsing.Statements
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public abstract class AbstractCrimsonStatement
    {
        public bool Mapped { get; set; }

        public abstract void Link (MappingContext context);

        public abstract IGeneralAssemblyStructure Generalise (GeneralisationContext context);

        public override string ToString ()
        {
            return GetType().Name;
        }
    }
}