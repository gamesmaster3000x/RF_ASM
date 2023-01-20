using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class UnitGeneratorException : FileNotFoundException
    {
        public UnitGeneratorException(string message) : base (message)
        {

        }

        public UnitGeneratorException(string message, System.Exception cause) : base(message, cause)
        {

        }
    }
}
