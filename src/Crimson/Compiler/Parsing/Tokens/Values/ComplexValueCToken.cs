
using Compiler.Mapping;

namespace Compiler.Parsing.Tokens.Values
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (MappingContext ctx);
    }
}