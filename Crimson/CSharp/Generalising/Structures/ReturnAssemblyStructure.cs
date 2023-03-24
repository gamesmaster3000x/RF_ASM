namespace Crimson.CSharp.Generalising.Structures
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
    }
}