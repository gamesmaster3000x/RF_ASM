using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class MemoryAddressToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        public MemoryAddressToken(string value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            Type = TokenType.ADDRESS;
            RawValue = value.Replace("*", "", StringComparison.Ordinal);
        }

        public override byte[] GetBytes()
        {
            byte[] ba = Convert.FromHexString(RawValue);
            if (ba.Length != meta.DataWidth)
            {
                throw new CompilationException("Data width " + ba.Length + " of MemoryAddressToken " + RawValue + " does not match expected width " + meta.DataWidth);
            }
            return Utils.FitToDataWidth(meta.DataWidth, ba);
        }

        public override bool HasCorrectSyntax(IToken[] following)
        {
            return true;
        }
    }
}
