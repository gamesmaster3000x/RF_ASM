using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class Condition: ICrimsonToken
    {
        public Condition(ResolvableValue value)
        {
            Value = value;
        }

        public ResolvableValue Value { get; }

        public IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>();
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}