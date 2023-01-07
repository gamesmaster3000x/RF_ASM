using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class ElseIfBlockCToken: ICrimsonToken
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

        public IList<BasicStatement> GetCrimsonBasic()
        {
            return IfBlock.GetCrimsonBasic();
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}