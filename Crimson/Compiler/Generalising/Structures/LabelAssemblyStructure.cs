namespace CrimsonCore.Generalising.Structures
{
    public class LabelAssemblyStructure : IGeneralAssemblyStructure
    {
        public string Text { get; protected set; }

        public LabelAssemblyStructure (string name)
        {
            Text = name;
        }

        public override string ToString ()
        {
            return "LAB: " + Text;
        }

        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }
    }
}