using Compiler.Generalising.Structures;
using Compiler.Generalising;
using Compiler.Mapping;

namespace Compiler.Parsing.Syntax
{
    internal class AssemblyCall : IAssemblable, IMappable
    {
        private string assemblyText;

        public AssemblyCall (string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ArbitraryAssemblyStructure structure = new ArbitraryAssemblyStructure(assemblyText);
            return structure;
        }

        public void Map (MappingContext context)
        {
        }

        public override string ToString ()
        {
            return assemblyText;
        }
    }
}