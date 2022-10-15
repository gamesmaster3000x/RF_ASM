using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Compiler
{
    internal class PreCompilationException : Exception
    {
        public PreCompilationException(string error) : base(error) { }
    }
}
