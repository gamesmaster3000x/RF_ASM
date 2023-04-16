using RedFoxAssembly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Statements
{
    internal class InstructionCommand : ICommand
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
            byte[] arg1value = new byte[0];
            byte[] arg2value = new byte[0]; ;

            instructionCode = (byte)_type;

            if (_type == InstructionType.JMP)
            {
                string k = _arg1!.ToString()!;
                LabelCommand l = compiler.Labels[k];
                arg1value = l.GetBytes(compiler);
            }
            else if (_type == InstructionType.BSR)
            {
                string k = _arg1!.ToString()!;
                LabelCommand l = compiler.Labels[k];
                arg1value = l.GetBytes(compiler);
            }
            else if (_type == InstructionType.BFG)
            {
                string k = _arg1!.ToString()!;
                LabelCommand l = compiler.Labels[k];
                arg1value = l.GetBytes(compiler);
                arg2value = _arg2!.GetBytes(compiler);
            }
            else
            {
                arg1value = _arg1 != null ? _arg1.GetBytes(compiler) : (new byte[0]);
                arg2value = _arg2 != null ? _arg2.GetBytes(compiler) : (new byte[0]);
            }

            // Set addressing modes
            // If targetting register, set addressing mode bit in the instruction
            if (_arg1 != null) instructionCode = (byte)(instructionCode | (_arg1.IsTargetingRegister() ? 0b10000000 : 0x00)); // code | 10000000
            if (_arg2 != null) instructionCode = (byte)(instructionCode | (_arg2.IsTargetingRegister() ? 0b01000000 : 0x00)); // code | 01000000

            bytes.Add(instructionCode);
            bytes.AddRange(arg1value);
            bytes.AddRange(arg2value);

            return bytes.ToArray();
        }

        public int GetPredictedLength(RFASMCompiler compiler)
        {
            int width = 0;

            width += 1; // Instruction code
            if (_arg1 != null) width += _arg1!.GetWidth(compiler);
            if (_arg2 != null) width += _arg2!.GetWidth(compiler);

            return width;
        }
    }
}
