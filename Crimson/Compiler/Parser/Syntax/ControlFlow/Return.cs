using Compiler.Generalising.Structures;
using Compiler.Parser.Syntax.Values;
using Compiler.Mapper;
using Compiler.Generaliser;

namespace Compiler.Parser.Syntax.ControlFlow
{
    internal class Return : IMappable, IAssemblable
    {
        public Return (ISimpleValue value)
        {
            Value = value;
        }

        public ISimpleValue Value { get; }

        public void Map (MappingContext ctx)
        {
        }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            return new ReturnAssemblyStructure();
        }
    }
}