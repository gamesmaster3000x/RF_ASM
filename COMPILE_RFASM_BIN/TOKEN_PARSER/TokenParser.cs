using RFASM_COMPILER.RFASM_BIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RFASM_COMPILER.TOKEN_PARSER
{
    internal class TokenParser
    {
        private Regex GOOD_TOKEN;
        private Regex IGNORE_TOKEN;
        private Regex BAD_TOKEN;
        private ITokenParserMetadata meta;
        private ITokenGenerator generator;

        public TokenParser(Regex goodToken, Regex ignoreToken, Regex badToken, ITokenParserMetadata meta, ITokenGenerator generator)
        {
            this.GOOD_TOKEN = goodToken;
            this.IGNORE_TOKEN = ignoreToken;
            this.BAD_TOKEN = badToken;
            this.meta = meta;
            this.generator = generator;
        }

        public List<Token>? Parse(List<string> input)
        {
            List<Token> tokens = new List<Token>();

            foreach(string line in input)
            {
                string line_ = IGNORE_TOKEN.Replace(line, "");

                if (BAD_TOKEN.IsMatch(line_))
                {
                    throw new TokenParsingException("Bad token on line: " + line_);
                }

                foreach (Match match in GOOD_TOKEN.Matches(line_))
                {
                    Token token = generator.GetToken(match.Value, meta);
                    tokens.Add(token);
                }
            }

            List<string> strings = new List<string>();
            foreach (Token token in tokens)
            {
                strings.Add(token.Value);
            }

            Console.WriteLine(String.Join(" ", strings));
            return tokens;
        }
    }
}
