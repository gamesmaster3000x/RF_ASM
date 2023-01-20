using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class ParserException : ArgumentException
    {
        public ParserException(string message) : base (message)
        {

        }

        public ParserException(string message, System.Exception cause) : base(message, cause)
        {

        }
    }
}
