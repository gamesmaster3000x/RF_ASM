namespace RedFoxVM
{
    internal class Word
    {
        private bool[] data;

        public Word(int dataWidth)
        {
            data = new bool[dataWidth * 8];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = false;
            }
        }

        public Word(bool[] data)
        {
            this.data = data;
        } 

        public bool this[int key]
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

        public static Word operator ++(Word a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i])
                {
                    a[i] = false;
                }
                else
                {
                    a[i] = true;
                    return a;
                }
            }
            return a;
        }

        public int Length { get { return data.Length; } }

        public byte[] Bytes
        {
            get
            {
                byte[] bytes = new byte[data.Length / 8];
                for (int i = 0; i < data.Length / 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        bytes[i] += (byte)(Convert.ToByte(data[8 * i + j]) * Math.Pow(2, j));
                    }
                }
                return bytes;
            }
        }

        public bool[] Bits
        {
            get { return data; }
        }
    }
}