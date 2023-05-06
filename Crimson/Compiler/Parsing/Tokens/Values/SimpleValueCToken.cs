using Compiler.Generalising;
using Compiler.Mapping;

namespace Compiler.Parsing.Tokens.Values
{
    public abstract class SimpleValueCToken : ICrimsonToken
    {
        public SimpleValueCToken ()
        {
        }
        public abstract void Link (MappingContext ctx);
        public abstract string GetText ();
        public abstract bool CanEvaluate ();
        public abstract object Evaluate (GeneralisationContext context);
    }
}