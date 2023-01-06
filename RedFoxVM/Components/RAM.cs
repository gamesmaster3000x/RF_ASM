using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM.Components
{
    internal class RAM
    {
        private byte[] data;

        public RAM()
        {
            data = new byte [Capacity];
            for (int i = 0; i < Capacity; i++)
            {
                data[i] = 0;
            }
        }

        public int Capacity { get { return (int)Math.Pow(2, Computer.DataWidth * 8); } }

        public byte GetByte(Word addr)
        {
            return data[addr.ToInt32()];
        }

        public void SetByte(Word addr, byte val)
        {
            data[addr.ToInt32()] = val;
        }
    }
}
