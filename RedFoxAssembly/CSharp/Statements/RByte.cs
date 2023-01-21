using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class RByte : IData
    {
        private byte? _data;
        private string? _value;

        public RByte(byte data)
        {
            _data = data;
        }

        public RByte(string value)
        {
            _value = value;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            if (_value != null)
                if (compiler.Constants.TryGetValue(_value, out IData? val))
                    return val.GetBytes(compiler);
                else
                    throw new CompilationException("Cannot get byte value of undeclared constant value " + val);

            if (_data != null)
                return new byte[] { (byte)_data }; // Casting byte? to byte implicitly makes C# sad
            else throw new CompilationException("Word cannot return a null byte array");
        }
    }
}
