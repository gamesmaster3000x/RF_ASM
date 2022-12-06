using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Computer
    {
        public int dataWidth;

        public Memory memory;
        public Value[] generalRegisters;
        public Value[] componentRegisters;
        public Stack stack;
        public ALU alu;

        public Value nextInstructionAddress;
        public byte currentInstruction;
        public Value operandA;
        public byte operandB;

        public Computer(int dataWidth = 1, int memorySize = 1024, byte[] ramState = null, int registerCount = 32, int componentLaneCount = 16, int stackSize = 256)
        {
            memory = new Memory(memorySize, ramState);
            generalRegisters = new Value[registerCount];
            componentRegisters = new Value[componentLaneCount];
            stack = new Stack(stackSize);
            alu = new ALU(dataWidth);
            nextInstructionAddress = new Value(dataWidth);
            currentInstruction = 0;
            operandA = new Value(dataWidth);
            operandB = 0;
            this.dataWidth = dataWidth;

            for (int i = 0; i < registerCount; i++)
            {
                generalRegisters[i] = new Value(dataWidth);
            }
            for (int i = 0; i < componentLaneCount; i++)
            {
                componentRegisters[i] = new Value(dataWidth);
            }
        }

        public void TriggerClock()
        {
            currentInstruction = memory.GetByte(nextInstructionAddress);
            BitArray bitArray = new BitArray(new byte[] { currentInstruction });
            switch(currentInstruction)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                case 26:
                    break;
                case 27:
                    break;
                case 28:
                    break;
                case 29:
                    break;
                case 30:
                    break;
                case 31:
                    break;
                default:
                    break;
            }
        }
    }
}
