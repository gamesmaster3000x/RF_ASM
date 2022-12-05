using RedFoxVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Memory
    {
        byte[] data;
        int dataWidth;
        public Memory(int capacity, int dataWidth, byte[] input = null)
        {
            this.dataWidth = dataWidth;

            if (input == null)
            {
                input = new byte[] { (byte)0};
            }

            data = new byte[capacity];

            for (int i = 0; i < capacity; i++)
            {
                if (i < input.Length)
                {
                    data[i] = input[i];
                }
                else
                {
                    data[i] = (byte)0;
                }
            }
        }

        public byte GetByte(int addr)
        {
            return data[addr];
        }

        public Value GetValue(int addr)
        {
            byte[] bytes = new byte[dataWidth];
            for (int i = 0; i < dataWidth; i++)
            {
                bytes[i] = data[addr + i];
            }
            return new Value(bytes);
        }
    }
}
