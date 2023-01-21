using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.TokenParser
{
    /// <summary>
    /// For example: "LDA"=Token(INSTRUCTION, "LDA") or ".val"=Token(DIRECTIVE, ".val") or "::label"=Token(LABEL, "::label")
    /// </summary>
    internal interface IToken
    {
        TokenType GetType();

        string GetRawValue();

        byte[] GetBytes();

        bool CheckFollowing(IToken[] following);
    }
}
