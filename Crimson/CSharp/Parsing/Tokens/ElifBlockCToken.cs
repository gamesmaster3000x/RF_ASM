using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Statements;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Tokens
{
    internal class ElseIfBlockCToken : ICrimsonToken
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifBlock">An IfBlock representing the "if" part of this statement (NOT THIS STATEMENT'S PARENT)</param>
        public ElseIfBlockCToken (IfBlockCStatement ifBlock)
        {
            IfBlock = ifBlock;
        }

        public IfBlockCStatement IfBlock { get; }

        public void Link (LinkingContext ctx)
        {
            IfBlock.Link(ctx);
        }
    }
}