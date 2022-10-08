using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class NameToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        public NameToken(string value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            Type = TokenType.NAME;
            RawValue = value.Replace("_", "", StringComparison.Ordinal);
        }

        public override byte[] GetBytes()
        {
            meta.constants.TryGetValue(RawValue, out byte[]? val);
            return Utils.FitToDataWidth(meta.DataWidth, val);
        }

        public override bool HasCorrectSyntax(IToken[] following)
        {
            return true;
        }

        
    }
}
