using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class LinkingException : ArgumentException
    {
        public LinkingException(string message) : base (message)
        {

        }

        public LinkingException(string message, System.Exception cause) : base(message, cause)
        {

        }
    }
}
