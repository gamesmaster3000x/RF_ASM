namespace Crimson.CSharp.Generalising.Structures
{
    internal class ScopeAssemblyStructure : IGeneralAssemblyStructure
    {
        public List<IGeneralAssemblyStructure> Structures { get; private set; }

        public ScopeAssemblyStructure ()
        {
        }

        internal void AddSubStructure (IGeneralAssemblyStructure labelAssemblyStructure)
        {
            Structures.Add(labelAssemblyStructure);
        }

        IEnumerable<IGeneralAssemblyStructure> IGeneralAssemblyStructure.GetSubStructures ()
        {
            return Structures;
        }
    }
}