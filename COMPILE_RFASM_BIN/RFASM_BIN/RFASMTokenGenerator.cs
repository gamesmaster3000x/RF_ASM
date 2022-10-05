using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN
{
    internal class RFASMTokenGenerator : ITokenGenerator
    {
        private ITokenParserMetadata meta;

        public RFASMTokenGenerator(ITokenParserMetadata meta)
        {
            this.meta = meta;
        }

        Token ITokenGenerator.GetToken(string value, ITokenParserMetadata meta)
        {
            return new Token(TokenType.INSTRUCTION, value);
        }
    }
}
