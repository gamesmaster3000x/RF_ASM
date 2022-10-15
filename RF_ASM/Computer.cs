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
        public byte[] memory;

        //Must be at least 4 elements, which are registers A, B, C and the top of the stack.
        public Value[] registers;

        public Stack stack;

        public Computer(uint dataWidth, uint memorySize, uint registerCount = 4, uint stackSize = 256)
        {
            memory = new byte[memorySize];
            registers = new Value[registerCount];
            for (int i = 0; i < registerCount; i++)
            {
                registers[i] = new Value(dataWidth);
            }
            stack = new Stack(stackSize);
        }
    }
}
