using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.TokenParser
{
    internal class TokenSyntaxException : Exception
    {
        public TokenSyntaxException(IToken[] tokens, int index)
            : base(String.Format("Incorrect syntax on line {1}: {0}", FormatTokenArray(tokens), index))
        {
        }

        public static string FormatTokenArray(IToken[] tokens)
        {
            List<string> rawValues = new List<string>();
            foreach(IToken token in tokens)
            {
                rawValues.Add(token.GetRawValue());
            }
            string str = String.Join(" ", rawValues);
            return str;
        }
    }
}
