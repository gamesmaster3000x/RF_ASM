using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Computer
    {
        public Memory memory;

        public Value[] generalRegisters;
        public Value[] specialisedRegisters;
        

        public Stack stack;

        public Computer(int dataWidth = 1, int memorySize = 1024, byte[] ramState = null, int registerCount = 32, int stackSize = 256)
        {
            memory = new Memory(memorySize, ramState);
            generalRegisters = new Value[registerCount];
            specialisedRegisters = new Value[128]; //Registers S63 - S127 are for communicating with components.
            for (int i = 0; i < registerCount; i++)
            {
                generalRegisters[i] = new Value(dataWidth);
            }
            stack = new Stack(stackSize);
        }

        
    }
}
