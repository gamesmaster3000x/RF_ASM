using CrimsonCore.Generalising;
using CrimsonCore.Linking;

namespace CrimsonCore.Parsing.Tokens.Values
{
    public abstract class SimpleValueCToken : ICrimsonToken
    {
        public SimpleValueCToken ()
        {
        }
        public abstract void Link (LinkingContext ctx);
        public abstract string GetText ();
        public abstract bool CanEvaluate ();
        public abstract object Evaluate (GeneralisationContext context);
    }
}