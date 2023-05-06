using Compiler.Generalising;
using Compiler.Generalising.Structures;
using Compiler.Mapping;

namespace Compiler.Parsing.Statements
{
    internal class BasicCallCStatement : AbstractCrimsonStatement
    {
        public string AssemblyText { get; protected set; }

        public BasicCallCStatement (string assemblyText)
        {
            AssemblyText = assemblyText;
        }
        public override void Link (MappingContext ctx)
        {
            Mapped = true;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ArbitraryAssemblyStructure structure = new ArbitraryAssemblyStructure(AssemblyText);
            return structure;
        }
    }
}