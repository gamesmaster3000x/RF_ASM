using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.TokenParser
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
