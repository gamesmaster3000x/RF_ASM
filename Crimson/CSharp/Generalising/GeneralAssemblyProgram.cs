using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Generalising
{
    public class GeneralAssemblyProgram
    {
        private List<IGeneralAssemblyStructure> Structures { get; set; }

        public void AddStructure (IGeneralAssemblyStructure structure)
        {
            Structures.Add(structure);
        }
    }
}
