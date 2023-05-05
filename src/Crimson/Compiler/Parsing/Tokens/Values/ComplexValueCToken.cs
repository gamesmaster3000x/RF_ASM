using CrimsonCore.Specialising;
using CrimsonCore.Linking;
using Compiler.Parsing.Tokens;

namespace Compiler.Parsing.Tokens.Values
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (LinkingContext ctx);
    }
}