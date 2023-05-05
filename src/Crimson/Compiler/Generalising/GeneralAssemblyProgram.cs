using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCore.Generalising
{
    public class GeneralAssemblyProgram
    {
        public List<IGeneralAssemblyStructure> Structures { get; private set; }

        public GeneralAssemblyProgram ()
        {
            Structures = new List<IGeneralAssemblyStructure>();
        }


        public void AddStructure (IGeneralAssemblyStructure structure)
        {
            Structures.Add(structure);
        }
    }
}
