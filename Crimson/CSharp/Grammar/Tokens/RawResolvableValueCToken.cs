
using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
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
    }
}