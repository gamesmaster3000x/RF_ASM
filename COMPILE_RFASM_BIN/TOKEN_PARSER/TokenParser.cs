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
        private ITokenParserMetadata meta;

        public TokenParser(ITokenParserMetadata meta)
        {
            this.meta = meta;
        }

        public List<Token>? Parse(List<string> input)
        {
            TokenReader reader = new TokenReader(input, new RFASMTokenGenerator(meta), meta);
            List<Token> tokens = new List<Token>();

            foreach(string token in input)
            {
                // Good Token: [A-Za-z0-9_]+((?=[ \n])|$) Match a string of alphanumeric characters (or underscore) followed by a space, newline or end of line
                // Ignore: /.+ Anything after a slash
                // Bad Token: [^A-Za-z0-9_] Contains anything which isn't alphanumeric (or underscore)
                string line = token;
                Regex GOOD_TOKEN = new Regex(@"[A-Za-z0-9_.:]+((?=[ \n])|$)");
                Regex IGNORE = new Regex(@"/.+");
                Regex BAD_TOKEN = new Regex(@"[^A-Za-z0-9_ .:]");

                Console.WriteLine(line);

                Console.WriteLine("Ignore: " + string.Join(" ", IGNORE.Matches(line).Cast<Match>().Select(m => m.Value).ToArray()));
                line = IGNORE.Replace(line, "");

                Console.WriteLine("Bad   : " + string.Join(" ", BAD_TOKEN.Matches(line).Cast<Match>().Select(m => m.Value).ToArray()));
                if (BAD_TOKEN.IsMatch(line))
                {
                    Console.WriteLine("Bad line!");
                }

                Console.WriteLine("Good  : " + string.Join(" ", GOOD_TOKEN.Matches(line).Cast<Match>().Select(m => m.Value).ToArray()));

                Console.WriteLine("");
            }

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
