namespace RedFoxVM.Components
{
    internal class Register
    {
        Word data;


        public Register(int dataWidth)
        {
            data = new Word(dataWidth);
        }


        public int Length
        {
            get { return data.Length / 8; }
        }


        public Word Word
        {
            get { return data; }
            set { data = value; }
        }

        public byte Byte
        {
            get { return data.Bytes[0]; }
            set 
            { 
                bool[] bits = data.Bits;
                byte b = value;
                for (int i = 0; i < 8; i++)
                {
                    if (b % 2 == 1)
                    {
                        bits[i] = true;
                    }
                    else
                    {
                        bits[i] = false;
                    }
                    b /= 2;
                }
                data = new Word(bits);
            }
        }
    }
}
