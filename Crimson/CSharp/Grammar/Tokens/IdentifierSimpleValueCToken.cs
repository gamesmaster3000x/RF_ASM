
using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class IdentifierSimpleValueCToken : SimpleValueCToken
    {
        public FullNameCToken Identifier { get; private set; }

        public IdentifierSimpleValueCToken (FullNameCToken identifier) : base(null) // NOT Identifier... need to get type from somewhere else
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
    }
}