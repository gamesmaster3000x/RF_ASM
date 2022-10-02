using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN.TokenisedParser
{
    /// <summary>
    /// For example: "LDA"=Token(INSTRUCTION, "LDA") or ".val"=Token(DIRECTIVE, ".val") or "::label"=Token(LABEL, "::label")
    /// </summary>
    internal class Token
    {
        private TokenType type;
        private string value;

        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public string Value { get => value; set => this.value = value; }
        internal TokenType Type { get => type; set => type = value; }

        public static Token GetToken(string value)
        {
            return new Token(TokenType.INSTRUCTION, value);
        }
    }
}
