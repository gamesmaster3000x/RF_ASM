using CrimsonCore.Specialising;
using CrimsonCore.Linking;

namespace CrimsonCore.Parsing.Tokens.Values
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (LinkingContext ctx);
    }
}