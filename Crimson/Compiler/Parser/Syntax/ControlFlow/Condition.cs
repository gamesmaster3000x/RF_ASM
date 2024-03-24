using Compiler.Mapper;
using Compiler.Parser.Syntax.Values;

namespace Compiler.Parser.Syntax.ControlFlow
{
    internal class Condition : IMappable
    {
        public Operation Operation { get; }

        public Condition (Operation operation)
        {
            Operation = operation;
        }

        public void Map (MappingContext ctx)
        {
            Operation.Map(ctx);
        }
    }
}