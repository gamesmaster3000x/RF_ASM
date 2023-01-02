using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public class ResolvableValue: ICrimsonToken
    {
        public ResolvableValue(object value)
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