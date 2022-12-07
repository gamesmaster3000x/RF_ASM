using System;
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
            
        }
    }
}
