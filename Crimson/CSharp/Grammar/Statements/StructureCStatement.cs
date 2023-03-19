using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Statements
{
    public class StructureCStatement : AbstractCrimsonStatement, INamed
    {
        public FullNameCToken Name { get; set; }

        public StructureCStatement (FullNameCToken name, IList<AbstractCrimsonStatement> body)
        {
            Name = name;
            Body = body;
        }

        public IList<AbstractCrimsonStatement> Body { get; }

        public override Fragment GetCrimsonBasic ()
        {
            return new Fragment(0);
        }

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }

        public FullNameCToken GetName ()
        {
            return Name;
        }

        public void SetName (FullNameCToken name)
        {
            Name = name;
        }

        public int GetSize ()
        {
            return -10000;
        }
    }
}