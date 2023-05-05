using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCore.Exceptions
{
    internal class GeneralisingException : ArgumentException
    {
        public GeneralisingException (string message) : base(message)
        {

        }

        public GeneralisingException (string message, Exception cause) : base(message, cause)
        {

        }
    }
}
