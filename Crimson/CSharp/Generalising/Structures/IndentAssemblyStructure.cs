namespace Crimson.CSharp.Generalising.Structures
{
    public class IndentAssemblyStructure : IGeneralAssemblyStructure
    {
        public int Indent { get; set; }

        public IndentAssemblyStructure (int indent)
        {
            Indent = indent;
        }

        public List<IGeneralAssemblyStructure>? GetSubStructures ()
        {
            return null;
        }

        public override string ToString ()
        {
            return "";
        }

        IEnumerable<IGeneralAssemblyStructure> IGeneralAssemblyStructure.GetSubStructures ()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }
    }
}