using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Statements;
using Crimson.CSharp.Grammar.Types;
using System.Text.RegularExpressions;

namespace Crimson.CSharp.Grammar.Tokens
{
    public abstract class CrimsonTypeCToken : ICrimsonToken
    {

        public FullNameCToken Name { get; private set; }

        public CrimsonTypeCToken (FullNameCToken name)
        {
            Name = name;
        }

        public abstract void Link (LinkingContext ctx);
        public abstract int GetSize ();

        public static CrimsonTypeCToken Parse (FullNameCToken name)
        {
            if (!name.HasMember()) throw new ArgumentNullException($"A name for a type must have a member name but was given {name}");

            if (name.MemberName.StartsWith('[') && name.MemberName.EndsWith(']'))
            {
                string inner = name.MemberName.Substring(1, name.MemberName.Length - 2);
                CrimsonTypeCToken type = Parse(new FullNameCToken(inner));
                return new ArrayCrimsonType(name, type);
            }

            if (GetRawtypeSize(name.MemberName) >= 0) return new RawtypeCrimsonType(name.MemberName);

            return new StructureCrimsonType(name);
        }

        internal static int GetRawtypeSize (string name)
        {
            return name.Trim() switch
            {
                "int" => Crimson.CSharp.Core.Crimson.Options.DataWidth,
                "byte" => 1,
                "ptr" => Crimson.CSharp.Core.Crimson.Options.DataWidth,
                "null" => 0,
                _ => -1,
            };
        }
    }
}