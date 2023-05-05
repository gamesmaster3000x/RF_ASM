using CrimsonCore.Specialising;
using CrimsonCore.Linking;
using Compiler.Generalising.Structures;
using Compiler.Generalising;

namespace Compiler.Parsing.Statements
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