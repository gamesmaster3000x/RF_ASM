using CrimsonCore.Specialising;
using CrimsonCore.Generalising;
using CrimsonCore.Generalising.Structures;
using CrimsonCore.Linking;

namespace CrimsonCore.Parsing.Statements
{
    internal class AssemblyCallCStatement : AbstractCrimsonStatement
    {
        private string assemblyText;

        public AssemblyCallCStatement (string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ArbitraryAssemblyStructure structure = new ArbitraryAssemblyStructure(assemblyText);
            return structure;
        }

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }

        public override string ToString ()
        {
            return assemblyText;
        }
    }
}