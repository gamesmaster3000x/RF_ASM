using Compiler.Mapping;

namespace Compiler.Parsing.Syntax.ControlFlow
{
    internal class ElseIfBlock : IMappable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifBlock">An IfBlock representing the "if" part of this statement (NOT THIS STATEMENT'S PARENT)</param>
        public ElseIfBlock (IfBlock ifBlock)
        {
            IfBlock = ifBlock;
        }

        public IfBlock IfBlock { get; }

        public void Map (MappingContext ctx)
        {
            IfBlock.Map(ctx);
        }
    }
}