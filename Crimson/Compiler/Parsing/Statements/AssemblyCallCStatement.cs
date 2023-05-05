using Compiler.Generalising.Structures;
using Compiler.Generalising;
using Compiler.Mapping;

namespace Compiler.Parsing.Statements
{
    internal class AssemblyCallCStatement : AbstractCrimsonStatement
    {
        private string assemblyText;

        public AssemblyCallCStatement (string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ArbitraryAssemblyStructure structure = new ArbitraryAssemblyStructure(assemblyText);
            return structure;
        }

        public override void Link (MappingContext ctx)
        {
            Mapped = true;
        }

        public override string ToString ()
        {
            return assemblyText;
        }
    }
}