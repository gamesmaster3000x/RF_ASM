using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Exceptions
{
    internal class NamingException: Exception
    {
        public NamingException(string message): base (message) { }
        public NamingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
