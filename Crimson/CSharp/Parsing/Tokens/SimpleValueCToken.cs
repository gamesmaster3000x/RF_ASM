using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Parsing.Tokens
{
    public abstract class SimpleValueCToken : ICrimsonToken
    {
        public SimpleValueCToken ()
        {
        }
        public abstract void Link (LinkingContext ctx);
        public abstract string GetText ();
        public abstract bool CanEvaluate ();
        public abstract object Evaluate ();
    }
}