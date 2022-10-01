using COMPILE_RFASM_BIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
    public class InstructionBuilders
    {
        public static byte[] HLT(string[] strings)
        {
            return new byte[] { Instructions.bHLT };
        }
        public static byte[] LDA(string[] strings)
        {
            RequireLength(Instructions.sLDA, strings, 2);
            return new byte[] { Instructions.bLDA, ToByte(strings[1]) };
        }

        // Util

        public static void RequireLength(string instruction, string[] arr, int requiredLength)
        {
            if (arr.Length < requiredLength)
            {
                throw new CompilationException(instruction + " requires at least " + requiredLength + " arguments (including the instruction itself)");
            }
        }

        public static byte ToByte(string instruction)
        {
            if (instruction.StartsWith("0x"))
            {
                instruction = instruction.Substring(2);
            }
            return Byte.Parse(instruction, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
