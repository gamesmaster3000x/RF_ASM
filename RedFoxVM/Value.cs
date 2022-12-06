using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Value
    {
        private byte[] data;

        public byte this[int key]
        {
            get
            {
                return data[key];
            }
            set
            {
                data[key] = value;
            }
        }

        public Value(int size)
        {
            data = new byte[size];
            for (int b = 0; b < size; b++)
            {
                data[b] = 0;
            }
        }

        public Value(byte[] data)
        {
            this.data = data;
        }

        public Value(Value val)
        {
            this.data = val.ToByteArray;
        }

        public static Value operator +(Value a)
        {
            return a;
        }

        public static Value operator -(Value a) //TODO: increment output to make value correct
        {
            Value o = new(a.Length);
            BitArray bits = new(a.data);
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = !bits[i];
            }
            for (int i = 0; i < o.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bits[8 * i + j])
                    {
                        o[i] += (byte)Math.Pow(2, j);
                    }
                }
            }
            return o;
        }

        public static Value operator +(Value a, Value b)
        {
            Value o;

            if (a.Length != b.Length)
            {
                throw new Exception("You cannot add two values of different lengths!");
            }

            o = new(a.Length);

            for (int i = 0; i < a.Length; i++)
            {
                if (byte.MaxValue - a[i] - o[i] < b[i])
                {
                    if(i + 1 < a.Length)
                    {
                        o[i + 1]++;
                    }
                }
                o[i] += (byte)(a[i] + b[i]);
            }

            return o;
        }


        public byte[] ToByteArray
        {
            get { return data; }
        }

        public int ToInt32
        {
            get
            {
                int o = 0;
                if (Length <= 4)
                {
                    for (int i = 0; i < Length; i++)
                    {
                        o += data[i] * (int)Math.Pow(2, i * 8);
                    }
                }
                return o;
            }
        }

        public long ToInt64
        {
            get
            {
                long o = 0;
                if (Length <= 8)
                {
                    for (int i = 0; i < Length; i++)
                    {
                        o += data[i] * (long)Math.Pow(2, i * 8);
                    }
                }
                return o;
            }
        }

        public string ToBinaryString
        {
            get
            {
                string o = "";
                BitArray bits = new(data);
                bool[] arr = new bool[bits.Length];
                bits.CopyTo(arr, 0);
                Array.Reverse(arr);
                for (int i = 0; i < arr.Length; i++)
                {
                    o += Convert.ToByte(arr[i]);
                }
                return o;
            }
        }

        public int Length
        {
            get { return data.Length; }
        }
    }
}
