using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public class ResolvableValueCToken: ICrimsonToken
    {
        public ResolvableValueCToken(object value)
        {
            Value = value;
        }

        public object Value { get; }

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