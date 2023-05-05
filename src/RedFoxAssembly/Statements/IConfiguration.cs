using RedFoxAssembly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Statements
{
    internal interface IConfiguration
    {
        internal void Resolve(RFASMCompiler compiler);
    }
}
