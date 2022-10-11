using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.TOKEN_PARSER
{
    internal enum TokenType
    {
        INSTRUCTION,
        DIRECTIVE,
        LABEL,
        VALUE,
        NAME
    }
}
