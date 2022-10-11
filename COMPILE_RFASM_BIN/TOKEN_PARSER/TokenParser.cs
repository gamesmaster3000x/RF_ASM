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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodToken">A Regular-Expression which returns true for (and only for) a valid token.</param>
        /// <param name="ignoreToken">A Regular-Expression which returns true for any token which should be ignored, for example comments (in C# that's anything after //)</param>
        /// <param name="badToken">A Regular-Expression which returns true if the input contains an invalid token or character.</param>
        /// <param name="meta"></param>
        /// <param name="generator"></param>
        public TokenParser(Regex goodToken, Regex ignoreToken, Regex badToken, ITokenParserMetadata meta, ITokenGenerator generator)
        {
            this.GOOD_TOKEN = goodToken;
            this.IGNORE_TOKEN = ignoreToken;
            this.BAD_TOKEN = badToken;
            this.meta = meta;
            this.generator = generator;
        }

        public List<IToken>? Parse(List<string> input)
        {
            List<IToken> tokens = new List<IToken>();

            foreach(string line in input)
            {
                string line_ = IGNORE_TOKEN.Replace(line, "");

                if (BAD_TOKEN.IsMatch(line_))
                {
                    throw new TokenParsingException("Bad token on line: " + line_);
                }

                foreach (Match match in GOOD_TOKEN.Matches(line_))
                {
                    IToken token = generator.GetToken(match.Value, meta);
                    tokens.Add(token);
                }
            }

            List<string> strings = new List<string>();
            foreach (IToken token in tokens)
            {
                strings.Add(token.GetRawValue());
            }

            Console.WriteLine(String.Format("Found {0} tokens", strings.Count));
            return tokens;
        }
    }
}
