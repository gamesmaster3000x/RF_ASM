using COMPILE_RFASM_BIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
    public class Instructions
    {
        // Halt the program
        // HLT
        public static string sHLT = "HLT";
        public static byte bHLT = 0x00;

        // Load to the A register
        // LDA α
        public static string sLDA = "LDA";
        public static byte bLDA = 0x01;
        public static byte[] LDA(string[] strings)
        {
            RequireLength(sLDA, strings, 2);
            return new byte[] { bLDA, ToByte(strings[1]) };
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
