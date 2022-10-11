using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN
{
    /// <summary>
    /// 
    /// The instruction set used in RFASM and their corrosponding purposes and hex codes.
    /// 
    /// </summary>
    internal enum Instructions
    {
        HLT, 
        LDR, 
        STM, 
        ADD, 
        SUB, 
        LSL,
        LSR, 
        CMP, 
        B, 
        BEQ,
        BLT, 
        BGT, 
        BOF, 
        BSR, 
        RTN, 
        STB
    }
}
