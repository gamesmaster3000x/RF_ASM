using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class ALU
    {
        public Word a;
        public Word b;
        public Word o;

        public bool lt = false;
        public bool gt = false;
        public bool eq = false;

        public ALU(int dataWidth)
        {
            a = new Word(dataWidth);
            b = new Word(dataWidth);
            o = new Word(dataWidth);
        }

        public void ADD()
        {
            o = a + b;
        }

        public void SUB()
        {
            o = a - b;
        }

        public void LSL()
        {
            o = a << 1;
        }

        public void LSR()
        {
            o = a >> 1;
        }

        public void NEG()
        {
            o = -a;
        }

        public void NOT()
        {
            o = new Word(a.Length);
            BitArray bits = new(a.ToByteArray());
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
        }

        public void CMP()
        {
            lt = a < b;
            gt = a > b;
            eq = a == b;
        }
    }
}
