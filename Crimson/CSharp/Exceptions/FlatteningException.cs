using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exceptions
{
    internal class FlatteningException : ArgumentException
    {
        public FlatteningException (string message) : base(message)
        {

        }

        public FlatteningException (string message, Exception cause) : base(message, cause)
        {

        }
    }
}
