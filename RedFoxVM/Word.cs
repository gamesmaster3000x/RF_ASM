namespace RedFoxVM
{
    internal class Word
    {
        private bool[] bits;

        public Word(int dataWidth)
        {
            bits = new bool[dataWidth * 8];
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = false;
            }
        }



        public bool this[int key]
        {
            get
            {
                return bits[key];
            }
            set
            {
                bits[key] = value;
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



        public int Length { get { return bits.Length; } }

        public byte[] bytes
        {
            get
            {
                byte[] bytes = new byte[bits.Length / 8];
                for (int i = 0; i < bits.Length / 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        bytes[i] += (byte)(Convert.ToByte(bits[8 * i + j]) * Math.Pow(2, j));
                    }
                }
                return bytes;
            }
        }
    }
}