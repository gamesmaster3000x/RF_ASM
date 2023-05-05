using CrimsonCore.Generalising.Structures;
using CrimsonCore.Parsing.Tokens.Values;
using CrimsonCore.Generalising;
using CrimsonCore.Linking;

namespace CrimsonCore.Parsing.Statements
{
    internal class ReturnCStatement : AbstractCrimsonStatement
    {
        public ReturnCStatement (SimpleValueCToken value)
        {
            Value = value;
        }

        public SimpleValueCToken Value { get; }

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            return new ReturnAssemblyStructure();
        }
    }
}