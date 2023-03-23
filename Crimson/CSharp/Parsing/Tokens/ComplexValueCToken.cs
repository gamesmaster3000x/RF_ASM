using Crimson.CSharp.Assembly;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Tokens
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken ()
        {
        }

        public abstract void Link (LinkingContext ctx);
        public abstract Fragment GetBasicFragment ();
    }
}