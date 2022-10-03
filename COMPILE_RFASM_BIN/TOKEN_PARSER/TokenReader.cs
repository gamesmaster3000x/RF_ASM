using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RFASM_COMPILER.TOKEN_PARSER
{
    internal class TokenReader
    {
        private List<string> lines;
        private ITokenGenerator generator;
        private ITokenParserMetadata meta;
        /// <summary>
        /// Sits at the "head" - the line which is about to be read but hasn't yet
        /// </summary>
        private int linePointer;
        /// <summary>
        /// Sits at the "head" - the character which is about to be read but hasn't yet
        /// </summary>
        private int charPointer;

        public static readonly Regex NON_WHITESPACE = new Regex(@"[^\s]");

        public TokenReader(List<string> lines, ITokenGenerator generator, ITokenParserMetadata meta)
        {
            this.lines = lines;
            this.generator = generator;
            this.meta = meta;
            Reset();
        }

        public void Reset()
        {
            this.linePointer = 0;
            this.charPointer = 0;
        }

        public bool HasNextChar()
        {
            return lines[linePointer].Length > charPointer;
        }

        public bool HasNextLine()
        {
            return lines.Count > linePointer;
        }

        public bool HasNextToken()
        {
            // True if there is a non-whitespace character remaining on this line
            if (charPointer < lines[linePointer].Length)
            {
                string remainingLine = lines[linePointer].Substring(charPointer);
                return NON_WHITESPACE.IsMatch(remainingLine);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The line which the linePointer is currently sitting on, then increments the linePointer</returns>
        public string NextLine()
        {
            if (HasNextLine())
            {
                charPointer = 0;
                return lines[linePointer++];
            }
            throw new TokenParsingException("Do not have the next line.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The character which the charPointer is currently sitting on, then increments the charPointer</returns>
        public char NextChar()
        {
            // If can get the next char on this line, returns it
            if (HasNextChar()) {
                return lines[linePointer].ToCharArray()[charPointer++]; 
            }
            throw new TokenParsingException("Do not have the next character.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The next token on the given line.</returns>
        public Token NextToken()
        {
            string value = "";

            // If there is a valid next character (line returns are not valid!)
            while (HasNextChar())
            {
                char c = NextChar();

                // If finds a comment, return what we have so far
                if (c == '/')
                {
                    NextLine();
                    break;
                }

                // If the recieved character is a space, the Token has ended
                if (c == ' ' || !HasNextChar())
                {
                    if (c != ' ')
                    {
                        value += c;
                    }
                    if (NON_WHITESPACE.IsMatch(value))
                    {
                        Token to = generator.GetToken(value, meta);
                        return to;
                    }

                    continue; // Continue so that it doesn't get added to the value
                }

                value += c;
            }

            // The next character cannot be gotten / is invalid, so return what we have.
            Token t = Token.GetToken(value);
            return t;

        }
    }
}
