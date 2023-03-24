using Crimson.CSharp.Generalising;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Tokens;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Statements
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

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            return new EmptyAssemblyStructure();
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