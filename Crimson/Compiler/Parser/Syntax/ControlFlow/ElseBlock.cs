using Compiler.Mapper;

namespace Compiler.Parser.Syntax.ControlFlow
{
    internal class ElseBlock : IMappable, IHasScope
    {
        public ElseBlock (Scope statements)
        {
            Scope = statements;
        }

        public Scope Scope { get; }

        public Scope GetScope () => Scope;

        public void Map (MappingContext ctx)
        {
            Scope.Map(ctx);
            return;
        }
    }
}