
using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class LabelDeclarationToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        public LabelDeclarationToken(string value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            TokenType = TokenType.LABEL;
            RawValue = value.Replace(":", "", StringComparison.Ordinal);
        }

        public override byte[] GetBytes()
        {
            return new byte[0];
        }

        public override bool CheckFollowing(IToken[] following)
        {
            return true;
        }

        internal List<IToken> DeclareLabel(RFASMCompiler compiler, int index)
        {
            string hexIndex = index.ToString("X" + meta.DataWidth * 2);
            meta.constants.Add(RawValue, new TokenTemplate(AddressingMode.RAW, hexIndex, meta)); // TODO DeclareLabel index might truncate things!!

            return new List<IToken>();
        }
    }
}
