namespace Compiler.Generalising
{
    public interface IGeneralAssemblyStructure
    {
        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ();
    }
}
