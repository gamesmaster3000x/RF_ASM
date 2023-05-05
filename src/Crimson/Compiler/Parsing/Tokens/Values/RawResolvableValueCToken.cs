using Compiler.Generalising;
using CrimsonCore.Linking;

namespace Compiler.Parsing.Tokens.Values
{
    public class RawResolvableValueCToken : SimpleValueCToken
    {
        public string Content { get; }

        public RawResolvableValueCToken (string s)
        {
            Content = s;
        }

        public override void Link (LinkingContext ctx)
        {
        }

        public override string ToString ()
        {
            return GetText();
        }

        public override string GetText ()
        {
            return Content;
        }

        public override object Evaluate (GeneralisationContext context)
        {
            return Content;
        }

        public override bool CanEvaluate ()
        {
            return true;
        }
    }
}