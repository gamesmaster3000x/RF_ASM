using CrimsonCore.Generalising.Structures;
using CrimsonCore.Specialising;
using CrimsonCore.Linking;
using Compiler.Parsing.Tokens;
using Compiler.Generalising;

namespace Compiler.Parsing.Statements
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
            Name = ctx.GetUniqueFunctionName(Name);
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