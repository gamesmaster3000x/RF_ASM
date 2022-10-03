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
    /// <list type="table">
    ///     <listheader>
    ///         <term>Code</term>
    ///         <term>Hex</term>
    ///         <term>Info</term>
    ///     </listheader>
    ///     <item>
    ///         <term>HLT</term>
    ///         <term>00</term>
    ///         <term>Immediately stop the execution of instructions.Next instruction will not be read.</term>
    ///     </item>
    ///     <item>
    ///         <term>LDA</term>
    ///         <term>01</term>
    ///         <term>Load the value from α to the A register.</term>
    ///     </item>
    ///     <item>
    ///         <term>LDB</term>
    ///         <term>02</term>
    ///         <term>Load the value from α to the B register.</term>
    ///     </item>
    ///     <item>
    ///         <term>ADD</term>
    ///         <term>03</term>
    ///         <term>Add the values in registers A and B, and store the result in C.</term>
    ///     </item>
    ///     <item>
    ///         <term>SUB</term>
    ///         <term>04</term>
    ///         <term>Subtract the value of register B from A and store the result in C.</term>
    ///     </item>
    ///     <item>
    ///         <term>CMP</term>
    ///         <term>05</term>
    ///         <term>Compare value at α to β.</term>
    ///     </item>
    ///     <item>
    ///         <term>B  </term>
    ///         <term>06</term>
    ///         <term>Jump to the memory address at α.</term>
    ///     </item>
    ///     <item>
    ///         <term>BEQ</term>
    ///         <term>07</term>
    ///         <term>If CMP returns equal, jump to the memory address at α.</term>
    ///     </item>
    ///     <item>
    ///         <term>BNE</term>
    ///         <term>08</term>
    ///         <term>If CMP returns not equal, jump to the memory address at α.</term>
    ///     </item>
    ///     <item>
    ///         <term>BGT</term>
    ///         <term>09</term>
    ///         <term>If CMP returns greater than, jump to the memory address at α.</term>
    ///     </item>
    ///     <item>
    ///         <term>BGT</term>
    ///         <term>0A</term>
    ///         <term>Push the next memory address to Stack, and jump to the memory address at α.</term>
    ///     </item>
    ///     <item>
    ///         <term>BGT</term>
    ///         <term>0B</term>
    ///         <term>Jump to the last memory address in stack, and remove it.</term>
    ///     </item>
    ///     <item>
    ///         <term>LDR</term>
    ///         <term>0C</term>
    ///         <term>Load the value from β into the register α.</term>
    ///     </item>
    ///     <item>
    ///         <term>CTM</term>
    ///         <term>0D</term>
    ///         <term>Move the value in C to the memory address at α.</term>
    ///     </item>
    ///     <item>
    ///         <term>CTM</term>
    ///         <term>0E</term>
    ///         <term>Move the value from register α to the memory address at β.</term>
    ///     </item>
    ///     <item>
    ///         <term>---</term>
    ///         <term>0F</term>
    ///         <term>---</term>
    ///     </item>
    /// </list>   
    /// </summary>
    internal enum Instructions
    {
        HLT,
        LDA,
        LDB,
        ADD,
        SUB,
        CMP,
        B,
        BEQ,
        BNE,
        BGT,
        BSR,
        RTN,
        LDR,
        CTM,
        RTM
    }
}
