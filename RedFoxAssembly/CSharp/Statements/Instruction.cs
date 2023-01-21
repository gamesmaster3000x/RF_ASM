using RedFoxAssembly.CSharp.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class Instruction
    {
        private InstructionType _type;
        private string? _arg1;
        private string? _arg2;

        public Instruction(InstructionType type, string? arg1, string? arg2)
        {
            _type = type;
            _arg1 = arg1;
            _arg2 = arg2;
        }
    }
}
