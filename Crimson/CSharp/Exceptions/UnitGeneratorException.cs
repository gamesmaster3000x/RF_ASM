using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exceptions
{
    internal class UnitGeneratorException : FileNotFoundException
    {
        public UnitGeneratorException (string message) : base(message)
        {

        }

        public UnitGeneratorException (string message, Exception cause) : base(message, cause)
        {

        }
    }
}
