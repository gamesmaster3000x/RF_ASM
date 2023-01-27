using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class ReturnCStatement : InternalStatement
    {
        public ReturnCStatement(SimpleValueCToken value)
        {
            Value = value;
        }

        public SimpleValueCToken Value { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new ReturnBStatement());
            return f;
        }
    }
}