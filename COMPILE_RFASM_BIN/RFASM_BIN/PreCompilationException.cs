using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN
{
    internal class PreCompilationException : Exception
    {
        public PreCompilationException(string error) : base(error) { }
    }
}
