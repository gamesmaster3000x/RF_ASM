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

        public virtual IList<string> GetDetailedMessage ()
        {
            return new List<string>() { "No additional message detail provided." };
        }

        public virtual IList<string>? FormatException (Exception? e)
        {
            return e?.ToString()?.Split(Environment.NewLine);
        }
    }
}
