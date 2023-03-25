using Crimson.CSharp.Linking;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Tokens.Values
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (LinkingContext ctx);
    }
}