using Crimson.CSharp.Assembly.RFASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Assembly
{
    public abstract class AbstractAssemblyProgram
    {
        internal abstract IEnumerable<Fragment> GetFragments ();
    }
}
