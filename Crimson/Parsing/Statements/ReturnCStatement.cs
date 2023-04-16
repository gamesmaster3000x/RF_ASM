using Crimson.Generalising.Structures;
using Crimson.Generalising;
using Crimson.Linking;
using Crimson.Parsing.Tokens.Values;

namespace Crimson.Parsing.Statements
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