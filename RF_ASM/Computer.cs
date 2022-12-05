using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
    internal class Computer
    {
        public byte[] memory;

        public Value[] generalRegisters;
        public Value[] specialisedRegisters;

        public Stack stack;

        public Computer(int dataWidth, int memorySize, int registerCount = 4, int stackSize = 256)
        {
            memory = new byte[memorySize];
            generalRegisters = new Value[registerCount];
            for (int i = 0; i < registerCount; i++)
            {
                generalRegisters[i] = new Value(dataWidth);
            }
            stack = new Stack(stackSize);
        }
    }
}
