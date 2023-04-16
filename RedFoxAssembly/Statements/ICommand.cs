using RedFoxAssembly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Statements
{
    internal interface ICommand
    {
        int GetPredictedLength(RFASMCompiler compiler);

        byte[] GetBytes(RFASMCompiler compiler);
    }
}
