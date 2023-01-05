using Crimson.CSharp.Core;
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

        public IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>() { new CommentBStatement($"Condition:{Value}") };
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}