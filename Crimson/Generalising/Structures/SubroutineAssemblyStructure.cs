using CrimsonCore.Generalising.Structures;
using System.Xml.Linq;

namespace CrimsonCore.Generalising.Structures
{
    internal class SubroutineAssemblyStructure : IGeneralAssemblyStructure
    {
        public string Name { get; private set; }

        private List<IGeneralAssemblyStructure> Structures { get; set; }

        public string EndName
        {
            get => $"end_{Name}";
            private set { }
        }

        public SubroutineAssemblyStructure (GeneralisationContext context, string name)
        {
            Name = name;
            Structures = new List<IGeneralAssemblyStructure>();
        }

        public void AddSubStructure (IGeneralAssemblyStructure labelAssemblyStructure)
        {
            Structures.Add(labelAssemblyStructure);
        }

        public override string ToString ()
        {
            return $"SUB:\n" +
                $"{string.Join("\n    ", Structures)}";
        }

        IEnumerable<IGeneralAssemblyStructure> IGeneralAssemblyStructure.GetSubStructures ()
        {
            return Structures // inner sub structures
                .Prepend(new CommentAssemblyStructure(Name)) // // NAME
                .Prepend(new JumpAssemblyStructure(EndName)) // JMP _END_NAME_
                .Prepend(new LabelAssemblyStructure(Name))   // ::__NAME__::
                .Append(new LabelAssemblyStructure(EndName)) // ::__END_NAME__::
                .Append(new CommentAssemblyStructure(""))    // // (newline)
                .ToList();
        }
    }
}