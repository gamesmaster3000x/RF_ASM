using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class ReturnCStatement : InternalStatement
    {
        public ReturnCStatement(ResolvableValueCToken value)
        {
            Value = value;
        }

        public ResolvableValueCToken Value { get; }

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