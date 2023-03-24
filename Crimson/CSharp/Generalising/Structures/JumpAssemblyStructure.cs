namespace Crimson.CSharp.Generalising.Structures
{
    public class JumpAssemblyStructure : IGeneralAssemblyStructure
    {
        public string Target { get; protected set; }

        public JumpAssemblyStructure (string target)
        {
            Target = target;
        }

        public override string ToString ()
        {
            return "Jump: " + Target;
        }

        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }
    }
}