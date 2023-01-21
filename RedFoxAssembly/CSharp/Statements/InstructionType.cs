using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    /// <summary>
    /// 
    /// The instruction set used in RFASM and their corrosponding purposes and hex codes.
    /// 
    /// </summary>
    internal enum InstructionType
    {
        HLT,
        NOP,
        ADD,
        SUB,
        LSL,
        LSR,
        NEG,
        NOT,
        CMP,
        JMP,
        BFG,
        //LSL,
        //LSL,
        //LSL,
        BSR,
        RTN,
        RRB,
        RRW,
        RMB,
        RMW,
        WRB,
        WRW,
        WMB,
        WMW,
        RVB,
        RVW,
        SIN,
        @INT,
        SFG,
        AND,
        LOR,
        XOR
    }
}
