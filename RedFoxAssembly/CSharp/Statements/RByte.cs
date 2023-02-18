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
        public bool TargetingRegister { get; protected set; }
        public byte? Data { get; protected set; }
        public string? Identifier { get; protected set; }

        public RByte(bool isTargetingRegister, byte data)
        {
            TargetingRegister = isTargetingRegister;
            Data = data;
        }

        public RByte(bool isTargetingRegister, string identifier)
        {
            TargetingRegister = isTargetingRegister;
            Identifier = identifier;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            // Return the value of the named constant 
            if (Identifier != null)
            {
                if (compiler.Constants.TryGetValue(Identifier, out IData? val))
                {
                    byte[] v = val.GetBytes(compiler);
                    if (v.Length != 1)
                        throw new CompilationException($"Cannot assign value [{String.Join(',', v)}] of constant {Identifier} to a byte (incorrect width {v.Length}.");
                    return v;
                }
                else
                {
                    throw new CompilationException("Cannot get byte value of undeclared constant value " + val);
                }
            }

            // Return _data
            if (Data != null)
            {
                return new byte[] { (byte)Data };
            }

            throw new CompilationException("Byte's data is null.");
        }

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(Identifier)) return Identifier;
            else if (Data != null) return String.Join("", Data!);
            else return "Word(Empty)";
        }

        public bool IsTargetingRegister()
        {
            return TargetingRegister;
        }

        public int GetWidth(RFASMCompiler compiler)
        {
            return 1;
        }

        public static byte[] MakeByteArr (List<RByte> rBytes)
        {
            byte[] bytes = new byte[rBytes.Count];
            for (int i = 0; i < rBytes.Count; i++)
                bytes[i] = (byte) rBytes[i].Data;
            return bytes;
        }
    }
}
