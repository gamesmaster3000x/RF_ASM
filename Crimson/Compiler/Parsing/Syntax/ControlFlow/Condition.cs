using Compiler.Mapping;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.Syntax.ControlFlow
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