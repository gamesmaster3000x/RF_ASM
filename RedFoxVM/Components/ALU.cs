﻿namespace RedFoxVM.Components
{
    internal class ALU
    {
        public void Add()
        {
            Word a = Computer.processor.registers[1].Word;
            Word b = Computer.processor.registers[2].Word;
            Word o = new Word(Computer.DataWidth);
            bool carry = false;
            for (int i = 0; i < a.Length; i++)
            {
                byte x = (byte)(Convert.ToByte(carry) + Convert.ToByte(a[i]) + Convert.ToByte(b[i]));
                if (x == 0)
                {
                    o[i] = false;
                    carry = false;
                }
                else if (x == 1)
                {
                    o[i] = true;
                    carry = false;
                }
                else if (x == 2)
                {
                    o[i] = false;
                    carry = true;
                }
                else
                {
                    o[i] = true;
                    carry = true;
                }
            }
            Computer.processor.flags[5] = carry;
            Computer.processor.registers[0].Word = o;
        }

        public void Subtract()
        {
            Word a = Computer.processor.registers[1].Word;
            Word b = Computer.processor.registers[2].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = !b[i];
            }
            b++;

            bool carry = false;
            for (int i = 0; i < Computer.processor.registers[1].Length * 8; i++)
            {
                byte x = (byte)(Convert.ToByte(carry) + Convert.ToByte(a[i]) + Convert.ToByte(b[i]));
                if (x == 0)
                {
                    o[i] = false;
                    carry = false;
                }
                else if (x == 1)
                {
                    o[i] = true;
                    carry = false;
                }
                else if (x == 2)
                {
                    o[i] = false;
                    carry = true;
                }
                else
                {
                    o[i] = true;
                    carry = true;
                }
            }
            Computer.processor.flags[5] = carry;
            Computer.processor.registers[0].Word = o;
        }

        public void ShiftLeft()
        {
            Word a = Computer.processor.registers[1].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 1; i < a.Length; i++)
            {
                o[o.Length - i] = a[a.Length - (i + 1)];
            }

            o[0] = false;

            Computer.processor.flags[5] = a[a.Length - 1];
            Computer.processor.registers[0].Word = o;
        }

        public void ShiftRight()
        {
            Word a = Computer.processor.registers[1].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 1; i < a.Length; i++)
            {
                o[i-1] = a[i];
            }

            o[o.Length - 1] = false;

            Computer.processor.flags[5] = a[0];
            Computer.processor.registers[0].Word = o;
        }

        public void Negate()
        {
            Word a = new Word(Computer.DataWidth);
            Word b = Computer.processor.registers[2].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = !b[i];
            }
            b++;

            bool carry = false;
            for (int i = 0; i < Computer.processor.registers[1].Length * 8; i++)
            {
                byte x = (byte)(Convert.ToByte(carry) + Convert.ToByte(a[i]) + Convert.ToByte(b[i]));
                if (x == 0)
                {
                    o[i] = false;
                    carry = false;
                }
                else if (x == 1)
                {
                    o[i] = true;
                    carry = false;
                }
                else if (x == 2)
                {
                    o[i] = false;
                    carry = true;
                }
                else
                {
                    o[i] = true;
                    carry = true;
                }
            }
            Computer.processor.flags[5] = carry;
            Computer.processor.registers[0].Word = o;
        }
        
        public void Not()
        {
            Word b = Computer.processor.registers[2].Word;
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = !b[i];
            }
            Computer.processor.registers[0].Word = b;
        }

        public void Compare()
        {
            Subtract();
            if (Computer.processor.registers[0].Word[Computer.processor.registers[0].Word.Length - 1])
            {
                Computer.processor.flags[3] = true;
                Computer.processor.flags[2] = false;
                Computer.processor.flags[4] = false;
            }
            else 
            {
                for (int i = 0; i < Computer.processor.registers[0].Word.Length; i++)
                {
                    if (Computer.processor.registers[0].Word[i])
                    {
                        Computer.processor.flags[4] = true;
                        Computer.processor.flags[2] = false;
                        Computer.processor.flags[3] = false;
                        return;
                    }
                }

                Computer.processor.flags[2] = true;
                Computer.processor.flags[3] = false;
                Computer.processor.flags[4] = false;
            }
        }

        public void And()
        {
            Word a = Computer.processor.registers[1].Word;
            Word b = Computer.processor.registers[2].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 0; i < a.Length; i++)
            {
                o[i] = a[i] & b[i];
            }

            Computer.processor.registers[0].Word = o;
        }

        public void Or()
        {
            Word a = Computer.processor.registers[1].Word;
            Word b = Computer.processor.registers[2].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 0; i < a.Length; i++)
            {
                o[i] = a[i] | b[i];
            }

            Computer.processor.registers[0].Word = o;
        }

        public void Xor()
        {
            Word a = Computer.processor.registers[1].Word;
            Word b = Computer.processor.registers[2].Word;
            Word o = new Word(Computer.DataWidth);

            for (int i = 0; i < a.Length; i++)
            {
                o[i] = a[i] ^ b[i];
            }

            Computer.processor.registers[0].Word = o;
        }
    }
}
