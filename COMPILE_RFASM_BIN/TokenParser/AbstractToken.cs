using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.TokenParser
{
    internal abstract class AbstractToken : IToken
    {
        private TokenType _tokenType;
        private string _rawValue;

        public TokenType TokenType { get => _tokenType; set => _tokenType = value; }
        public string RawValue { get => _rawValue; set => _rawValue = value; }

        TokenType IToken.GetType()
        {
            return TokenType;
        }

        string IToken.GetRawValue()
        {
            return RawValue;
        }

        public override string ToString()
        {
            return RawValue;
        }

        public abstract bool CheckFollowing(IToken[] following);
        public abstract byte[] GetBytes();

        public AbstractToken Clone()
        {
            return (AbstractToken) this.MemberwiseClone();
        }
    }
}
