namespace Crimson.CSharp.Statements
{
    internal class ElseIfBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifBlock">An IfBlock representing the "if" part of this statement (NOT THIS STATEMENT'S PARENT)</param>
        public ElseIfBlock(IfBlock ifBlock)
        {
            IfBlock = ifBlock;
        }

        public IfBlock IfBlock { get; }
    }
}