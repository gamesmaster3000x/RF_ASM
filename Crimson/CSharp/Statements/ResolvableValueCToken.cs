using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
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

        public Fragment GetCrimsonBasic()
        {
            return new Fragment(0);
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }
    }
}