using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class StatementParseException: ArgumentException
    {
        public StatementParseException(string message) : base (message)
        {

        }

        public StatementParseException(string message, SystemException cause) : base(message, cause)
        {

        }
    }
}
