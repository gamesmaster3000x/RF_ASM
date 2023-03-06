using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Statements
{
    public class StructureCStatement : INamedStatement
    {
        public FullNameCToken Name { get; set; }

        public StructureCStatement(FullNameCToken name, IList<ICrimsonStatement> body)
        {
            Name = name;
            Body = body;
        }

        public IList<ICrimsonStatement> Body { get; }

        public Fragment GetCrimsonBasic ()
        {
            throw new NotImplementedException();
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }

        public FullNameCToken GetName ()
        {
            return Name;
        }

        public void SetName (FullNameCToken name)
        {
            Name = name;
        }
    }
}