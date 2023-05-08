using Compiler.Generaliser;

namespace Compiler.Generaliser.Structures
{
    public class ReturnAssemblyStructure : IGeneralAssemblyStructure
    {
        public ReturnAssemblyStructure ()
        {
        }

        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }

        public override string ToString ()
        {
            return "RET";
        }
    }
}