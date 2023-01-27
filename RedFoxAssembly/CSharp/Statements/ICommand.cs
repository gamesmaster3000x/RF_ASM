using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal interface ICommand
    {
        int GetPredictedLength(RFASMCompiler compiler);

        byte[] GetBytes(RFASMCompiler compiler);
    }
}
