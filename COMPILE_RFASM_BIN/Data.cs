using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN
{
    internal class Data
    {
        private byte[] value;

        public Data(byte[] value)
        {
            this.value = value;
        }

        /// <summary>
        /// Takes a value, for example <code>0x0f0f</code>, and creates a Data object to wrap it.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Data Parse(string value)
        {
            // Remove preceeding 0x
            if (value.StartsWith("0x", StringComparison.Ordinal))
            {
                value = value.Substring(2);
            }

            // Must be length multiple of 2 (whole byte)
            if (value.Length % 2 != 0)
            {

            }
            return null;
        }
    }
}
