using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Types
{
    internal class ArrayCrimsonType : CrimsonTypeCToken
    {
        private CrimsonTypeCToken type;

        public ArrayCrimsonType (FullNameCToken name, CrimsonTypeCToken type) : base(name)
        {
            this.type = type;
        }

        public override int GetSize ()
        {
            return type.GetSize() * -1000;
        }

        public override void Link (LinkingContext ctx)
        {
        }
    }
}