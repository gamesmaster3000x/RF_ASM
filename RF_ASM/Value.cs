using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Value
    {
        public byte[] data;

        public Value(uint size)
        {
            data = new byte[size];
        }

        public Value(byte[] data)
        {
            this.data = data;
        }
    }
}
