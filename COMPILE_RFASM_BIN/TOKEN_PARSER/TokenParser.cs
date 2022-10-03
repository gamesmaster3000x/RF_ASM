using RFASM_COMPILER.RFASM_BIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.TOKEN_PARSER
{
    internal class TokenParser
    {
        private ITokenParserMetadata meta;

        public TokenParser(ITokenParserMetadata meta)
        {
            this.meta = meta;
        }

        public List<Token>? Parse(List<string> input)
        {
            TokenReader reader = new TokenReader(input, new RFASMTokenGenerator(meta), meta);
            List<Token> tokens = new List<Token>();

            while (reader.HasNextLine())
            {
                while (reader.HasNextToken())
                {
                    Token t = reader.NextToken();

                    // If null, dont add!
                    if (t != null && t.Value != "")
                    {
                        tokens.Add(t); 
                    }
                }
                reader.NextLine();
            }

            return tokens;
        }
    }
}
