using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class LexerException : ArgumentException
    {
        public LexerException(string message) : base (message)
        {

        }

        public LexerException(string message, System.Exception cause) : base(message, cause)
        {

        }
    }
}
