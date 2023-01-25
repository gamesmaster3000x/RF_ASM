
using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public class IdentifierSimpleValueCToken : SimpleValueCToken
    {
        public string Identifier { get; private set; }

        public IdentifierSimpleValueCToken(string identifier)
        {
            Identifier = identifier;
        }

        public override void Link(LinkingContext ctx)
        {
            Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
        }

        public override string GetText()
        {
            return Identifier;
        }
    }
}