using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCore.Generalising
{
    public interface IGeneralAssemblyStructure
    {
        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ();
    }
}
