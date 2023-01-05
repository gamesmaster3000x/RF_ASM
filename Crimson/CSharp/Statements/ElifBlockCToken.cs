using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class ElseIfBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifBlock">An IfBlock representing the "if" part of this statement (NOT THIS STATEMENT'S PARENT)</param>
        public ElseIfBlock(IfBlockCStatement ifBlock)
        {
            IfBlock = ifBlock;
        }

        public IfBlockCStatement IfBlock { get; }

        internal void Link(LinkingContext ctx)
        {
            return;
        }
    }
}