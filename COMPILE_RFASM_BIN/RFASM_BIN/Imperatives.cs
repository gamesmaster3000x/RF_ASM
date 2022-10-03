using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN
{
    /// <summary>
    /// 
    /// Imperatives for the compiler during compile time.
    /// 
    /// <list type="table">
    ///     <listheader>
    ///         <term>Phrase</term>
    ///         <term>Args</term>
    ///         <term>Function</term>
    ///     </listheader>
    ///     <item>
    ///         <term>.jump/term>
    ///         <term>MemAddr</term>
    ///         <term>Jumps the program to the given address in memory</term>
    ///     </item>
    ///     <item>
    ///         <term>.jump/term>
    ///         <term>MemAddr</term>
    ///         <term>Jumps the program to the given address in memory</term>
    ///     </item>
    /// </list>
    /// </summary>
    enum Imperatives
    {
        LABEL,
        WIDTH,
        VAL
    }
}
