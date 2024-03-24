﻿namespace RedFoxVM.Components
{
    internal class RAM
    {
        private byte[] data;

        public RAM(byte[] data)
        {
            this.data = new byte[Capacity];
            for (int i = 0; i < Capacity; i++)
            {
                if (i < data.Length)
                {
                    this.data[i] = data[i];
                }
                else
                {
                    this.data[i] = 0;
                }
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
