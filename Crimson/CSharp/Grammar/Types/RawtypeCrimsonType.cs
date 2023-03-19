using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Types
{
    internal class RawtypeCrimsonType : CrimsonTypeCToken
    {
        public RawtypeCrimsonType (string name) : base(new FullNameCToken(name))
        {
        }

        public override int GetSize ()
        {
            return GetRawtypeSize(Name.MemberName);
        }

        public override void Link (LinkingContext ctx)
        {
            // No linking required
        }
    }
}