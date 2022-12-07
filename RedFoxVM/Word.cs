using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Word
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

        public Word(int size)
        {
            data = new byte[size];
            for (int b = 0; b < size; b++)
            {
                data[b] = 0;
            }
        }

        public Word(byte[] data)
        {
            this.data = data;
        }

        public Word(Word val)
        {
            data = val.ToByteArray();
        }
        public Word(int num, int width)
        {
            data = new byte[width];
            for (int i = 0; i < width; i++)
            {
                data[i] = (byte)(num % 256);
                num /= 256;
            }
        }

        public Word(BitArray bits)
        {
            data = new byte[((bits.Length - 1) / 8) + 1];
            for (int i = 0; i < Length; i++)
            {
                data[i] = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (bits[8 * i + j])
                    {
                        data[i] += (byte)Math.Pow(2, j);
                    }
                }
            }
        }

        public static Word operator +(Word a)
        {
            return a;
        }

        public static Word operator -(Word a)
        {
            Word o = new(a.Length);
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
            o++;
            return o;
        }

        public static Word operator +(Word a, Word b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("You cannot add two values of different lengths!");
            }

            Word o = new(a.Length);

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

        public static Word operator -(Word a, Word b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("You cannot add two values of different lengths!");
            }

            Word o = new(a.Length);

            o = a + -b;
            return o;
        }

        public static Word operator ++(Word a)
        {
            return a + One(a.Length);
        }

        public static Word operator --(Word a)
        {
            return a - One(a.Length);
        }

        public static Word operator <<(Word a, int b)
        {
            BitArray arr = new BitArray(a.ToByteArray());

            arr.LeftShift(b);

            return new(arr);
        }

        public static Word operator >>(Word a, int b)
        {
            BitArray arr = new BitArray(a.ToByteArray());

            arr.RightShift(b);

            return new(arr);
        }

        public byte[] ToByteArray()
        {
            return data;
        }

        public int ToInt32()
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

        public long ToInt64()
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

        public string ToBinaryString()
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

        public string ToHexString()
        {
            return Convert.ToHexString(data);
        }

        public override string ToString()
        {
            return string.Join('.', data);
        }

        public int Length
        {
            get { return data.Length; }
        }

        public static Word Zero(int length)
        {
            return new(length);
        }

        public static Word One(int length)
        {
            Word o = new(length);
            o[0] = 1;
            return o;
        }

        public static Word MaxValue(int length)
        {
            Word o = new(length);
            for (int i = 0; i < length; i++)
            {
                o[i] = byte.MaxValue;
            }
            return o;
        }
    }
}
