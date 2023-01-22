using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal interface IData
    {
        public byte[] GetBytes(RFASMCompiler compiler);
        public bool IsTargettingRegister();
    }
}
