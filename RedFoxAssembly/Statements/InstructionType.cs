using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Statements
{
    /// <summary>
    /// 
    /// The instruction set used in RFASM and their corrosponding purposes and hex codes.
    /// 
    /// </summary>
    internal enum InstructionType
    {
        HLT = 0,
        NOP = 1,
        ADD = 2,
        SUB = 3,
        LSL = 4,
        LSR = 5,
        NEG = 6,
        NOT = 7,
        CMP = 8,
        JMP = 9,
        BFG = 10,
        //LSL  = 11,
        //LSL  = 12,
        //LSL  = 13,
        BSR = 14,
        RTN = 15,
        RRB = 16,
        RRW = 17,
        RMB = 18,
        RMW = 19,
        WRB = 20,
        WRW = 21,
        WMB = 22,
        WMW = 23,
        RVB = 24,
        RVW = 25,
        SIN = 26,
        @INT = 27,
        SFG = 28,
        AND = 29,
        LOR = 30,
        XOR = 31
    }
}
