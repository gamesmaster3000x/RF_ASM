using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class InstructionCommand: ICommand
    {
        private InstructionType _type;
        private IData? _arg1;
        private IData? _arg2;

        public InstructionCommand(InstructionType type, IData? arg1, IData? arg2)
        {
            _type = type;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            List<byte> bytes = new List<byte>();
            byte instructionCode;
            byte[] arg1value;
            byte[] arg2value;

            Console.WriteLine("Addressing modes not yet implemented in ICommand.GetBytes (InstructionCommand?)");
            instructionCode = (byte) _type;
            arg1value = _arg1 != null ? _arg1.GetBytes(compiler) : new byte[0];
            arg2value = _arg2 != null ? _arg2.GetBytes(compiler) : new byte[0];

            bytes.Add(instructionCode);
            bytes.AddRange(arg1value);
            bytes.AddRange(arg2value);

            return bytes.ToArray();
        }
    }
}
