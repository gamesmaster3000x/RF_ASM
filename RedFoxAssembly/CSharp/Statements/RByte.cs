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
        private bool _isTargetingRegister;
        private byte? _data;
        private string? _identifier;

        public RByte(bool isTargetingRegister, byte data)
        {
            _isTargetingRegister = isTargetingRegister;
            _data = data;
        }

        public RByte(bool isTargetingRegister, string identifier)
        {
            _isTargetingRegister = isTargetingRegister;
            _identifier = identifier;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            // Return the value of the named constant 
            if (_identifier != null)
            {
                if (compiler.Constants.TryGetValue(_identifier, out IData? val))
                {
                    byte[] v = val.GetBytes(compiler);
                    if (v.Length != 1)
                        throw new CompilationException($"Cannot assign value [{String.Join(',', v)}] of constant {_identifier} to a byte (incorrect width {v.Length}.");
                    return v;
                }
                else
                {
                    throw new CompilationException("Cannot get byte value of undeclared constant value " + val);
                }
            }

            // Return _data
            if (_data != null)
            {
                return new byte[] { (byte)_data };
            }

            throw new CompilationException("Byte's data is null.");
        }

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(_identifier)) return _identifier;
            else if (_data != null) return String.Join("", _data!);
            else return "Word(Empty)";
        }

        public bool IsTargetingRegister()
        {
            return _isTargetingRegister;
        }

        public int GetWidth(RFASMCompiler compiler)
        {
            return 1;
        }
    }
}
