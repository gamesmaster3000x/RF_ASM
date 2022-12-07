using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class ALU
    {
        public Word inA;
        public Word inB;
        public Word output;

        public ALU(int dataWidth)
        {
            inA = new Word(dataWidth);
            inB = new Word(dataWidth);
            output = new Word(dataWidth);
        }

        /*public void ADD()
        {
            for (int i = 0; i < output.data.Length; i++)
            {
                if (true)
                {

                }
            }
        }*/
    }
}
