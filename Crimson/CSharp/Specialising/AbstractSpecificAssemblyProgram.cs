using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Specialising
{
    public abstract class AbstractSpecificAssemblyProgram
    {

        public abstract IEnumerable<Fragment> GetFragments ();
        public abstract string GetExtension ();
        public abstract void Write (string path);
    }
}
