using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN
{
    internal class ParsingException: Exception
    {
        public ParsingException(string line, string error): base(String.Format("{0} while parsing line '{1}'", error, line )){}

        public ParsingException(string error) : base(error){}
    }
}
