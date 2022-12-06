using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class ALU
    {
        public Value inA;
        public Value inB;
        public Value output;

        public ALU(int dataWidth)
        {
            inA = new Value(dataWidth);
            inB = new Value(dataWidth);
            output = new Value(dataWidth);
        }

        
    }
}
