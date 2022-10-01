using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
    internal class Instructions
    {
        // Halt the program
        // HLT
        public static string sHLT = "HLT";
        public static byte bHLT = 0x00;

        // Load to the A register
        // LDA α
        public static string sLDA = "LDA";
        public static byte bLDA = 0x01;
    }
}
