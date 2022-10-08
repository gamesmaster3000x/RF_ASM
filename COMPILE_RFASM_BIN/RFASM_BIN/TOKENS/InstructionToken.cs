using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class InstructionToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        private Instructions value;
        public InstructionToken(Instructions value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            this.value = value;
            Type = TokenType.INSTRUCTION;
            RawValue = Enum.GetName(value.GetType(), value);
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[] { (byte)value };
            return Utils.FitToDataWidth(meta.DataWidth, bytes);
        }

        // TODO InstructionToken syntax
        public override bool HasCorrectSyntax(IToken[] following)
        {
            return true;
        }
    }
}
