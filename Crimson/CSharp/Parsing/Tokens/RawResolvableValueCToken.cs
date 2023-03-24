using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Parsing.Tokens
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

        public override string GetText ()
        {
            return Content;
        }

        public override object Evaluate ()
        {
            return Content;
        }

        public override bool CanEvaluate ()
        {
            return true;
        }
    }
}