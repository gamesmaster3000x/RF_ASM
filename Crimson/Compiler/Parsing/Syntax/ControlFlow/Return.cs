using Compiler.Generalising.Structures;
using Compiler.Mapping;
using Compiler.Generalising;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.Syntax.ControlFlow
{
    internal class Return : IMappable, IAssemblable
    {
        public Return(ISimpleValue value)
        {
            Value = value;
        }

        public ISimpleValue Value { get; }

        public void Map(MappingContext ctx)
        {
        }

        public IGeneralAssemblyStructure Generalise(GeneralisationContext context)
        {
            return new ReturnAssemblyStructure();
        }
    }
}