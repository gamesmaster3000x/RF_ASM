using Crimson.CSharp.Linking;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Tokens
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (LinkingContext ctx);
    }
}