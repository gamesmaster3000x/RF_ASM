using Compiler.Mapper;
using Compiler.Generaliser;
using Compiler.Generaliser.Structures;

namespace Compiler.Parser.Syntax
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