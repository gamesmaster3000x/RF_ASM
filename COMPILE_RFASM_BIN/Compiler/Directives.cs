using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Compiler
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
    enum Directives
    {
        WIDTH,
        VAL
    }
}
