using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN.TokenisedParser
{
    internal class TokenisedParser
    {
        private Metadata meta;

        public TokenisedParser(Metadata meta)
        {
            this.meta = meta;
        }

        public List<Token>? Parse(List<string> input)
        {
            TokenReader reader = new TokenReader(input, meta);
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
