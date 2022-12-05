using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Value
    {
        public byte[] data;

        public Value(int size)
        {
            data = new byte[size];
        }

        public Value(byte[] data)
        {
            this.data = data;
        }

        public byte GetByte(int i)
        {
            return data[i];
        }
    }
}
