
using Compiler.Mapping;

namespace Compiler.Parsing.Tokens
{
    internal class ElseBlockCToken : ICrimsonToken, IHasScope
    {
        public ElseBlockCToken (Scope statements)
        {
            Scope = statements;
        }

        public Scope Scope { get; }

        public Scope GetScope () => Scope;

        public void Link (MappingContext ctx)
        {
            Scope.Link(ctx);
            return;
        }
    }
}