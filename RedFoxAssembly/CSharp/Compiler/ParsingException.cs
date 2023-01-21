using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Compiler
{
    internal class ParsingException : Exception
    {
        public ParsingException(string error) : base(error) { }
        public ParsingException(string error, Exception cause) : base(error, cause) { }
    }
}
