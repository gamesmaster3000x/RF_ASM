using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Specialising
{
    public abstract class AbstractSpecificAssemblyProgram
    {
        internal abstract IEnumerable<Fragment> GetFragments ();
    }
}
