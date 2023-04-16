using Crimson.Specialising;
using Crimson.Linking;

namespace Crimson.Parsing.Tokens.Values
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (LinkingContext ctx);
    }
}