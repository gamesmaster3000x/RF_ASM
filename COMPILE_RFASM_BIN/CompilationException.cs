using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN
{
    internal class CompilationException: Exception
    {
        public CompilationException(string line, string error): base(String.Format("{0} while compiling line '{1}'", error, line )){}

        public CompilationException(string error) : base(error)
        {

        }
    }
}
