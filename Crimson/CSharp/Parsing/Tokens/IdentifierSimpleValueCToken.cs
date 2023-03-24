using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Parsing.Tokens
{
    public class IdentifierSimpleValueCToken : SimpleValueCToken
    {
        public FullNameCToken Identifier { get; private set; }

        public IdentifierSimpleValueCToken (FullNameCToken identifier)
        {
            Identifier = identifier;
        }

        public override void Link (LinkingContext ctx)
        {
            Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
        }

        public override string GetText ()
        {
            return Identifier.ToString();
        }

        public override bool CanEvaluate ()
        {
            return false;
        }

        public override object Evaluate ()
        {
            return Identifier;
        }
    }
}