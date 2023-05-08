namespace Compiler.Generaliser
{
    public interface IGeneralAssemblyStructure
    {
        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ();
    }
}
