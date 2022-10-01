using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
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
