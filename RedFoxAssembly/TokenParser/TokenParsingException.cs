using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.TokenParser
{
    internal class TokenParsingException : Exception
    {
        public TokenParsingException(string line, string error) : base(string.Format("{0} while parsing line '{1}'", error, line)) { }

        public TokenParsingException(string error) : base(error) { }
    }
}
