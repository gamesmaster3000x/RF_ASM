using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exceptions
{
    internal abstract class CrimsonException : Exception
    {
        public CrimsonException (Core.Crimson.PanicCode code)
        {
            Code = code;
        }

        public Core.Crimson.PanicCode Code { get; private set; }
        public abstract IList<string> GetDetailedMessage ();

        public List<string> FormatException (Exception e)
        {

        }
    }
}
