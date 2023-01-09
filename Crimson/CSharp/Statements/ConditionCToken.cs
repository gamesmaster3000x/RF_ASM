using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class ConditionCToken: ICrimsonToken
    {
        public ConditionCToken(ResolvableValueCToken value)
        {
            Value = value;
        }

        public ResolvableValueCToken Value { get; }

        public Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement($"Condition:{Value}"));
            return f;
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}