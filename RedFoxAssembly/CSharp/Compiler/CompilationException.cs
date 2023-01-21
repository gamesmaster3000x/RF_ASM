using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Compiler
{
    internal class CompilationException : Exception
    {
        public CompilationException(string line, string error) : base(string.Format("{0} while compiling line '{1}'", error, line)) { }

        public CompilationException(string error) : base(error) { }
    }
}
