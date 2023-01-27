using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Tokens
{
    internal class ElseIfBlockCToken : ICrimsonToken
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifBlock">An IfBlock representing the "if" part of this statement (NOT THIS STATEMENT'S PARENT)</param>
        public ElseIfBlockCToken(IfBlockCStatement ifBlock)
        {
            IfBlock = ifBlock;
        }

        public IfBlockCStatement IfBlock { get; }

        public Fragment GetCrimsonBasic()
        {
            return IfBlock.GetCrimsonBasic();
        }

        public void Link(LinkingContext ctx)
        {
            IfBlock.Link(ctx);
        }
    }
}