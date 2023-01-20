using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class FlatteningException : ArgumentException
    {
        public FlatteningException(string message) : base (message)
        {

        }

        public FlatteningException(string message, System.Exception cause) : base(message, cause)
        {

        }
    }
}
