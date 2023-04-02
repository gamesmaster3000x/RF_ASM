using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exceptions
{
    internal class CrimsonAntlrTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
