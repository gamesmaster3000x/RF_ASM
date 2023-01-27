using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class CrimsonParserException : ArgumentException
    {
        public CrimsonParserException(string message) : base (message)
        {

        }

        public CrimsonParserException(string message, System.Exception cause) : base(message, cause)
        {

        }
    }
}
