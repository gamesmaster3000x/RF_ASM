using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Computer
    {
        public int dataWidth;
        public bool halt = false;

        public Memory memory;
        public Word[] generalRegisters;
        public Word[] componentRegisters;
        public Word[] interruptAddresses;
        public Stack stack;
        public ALU alu;


        public Word programCounter;
        public byte currentInstruction;
        public Word operandA;
        public bool opAMode = false;
        public byte operandB;
        public bool opBMode = false;

        public Computer(byte[] ramState, int dataWidth = 1, int memorySize = 1024, int registerCount = 32, int componentLaneCount = 16, int stackSize = 256, int interruptCount = 256)
        {
            memory = new Memory(memorySize, ramState);

            generalRegisters = new Word[registerCount];
            componentRegisters = new Word[componentLaneCount];
            interruptAddresses = new Word[interruptCount];

            stack = new Stack(stackSize);

            alu = new ALU(dataWidth);

            programCounter = new Word(dataWidth);
            currentInstruction = 0;
            operandA = new Word(dataWidth);
            operandB = 0;
            this.dataWidth = dataWidth;

            for (int i = 0; i < registerCount; i++)
            {
                generalRegisters[i] = new Word(dataWidth);
            }
            for (int i = 0; i < componentLaneCount; i++)
            {
                componentRegisters[i] = new Word(dataWidth);
            }
            for (int i = 0; i < interruptCount; i++)
            {
                interruptAddresses[i] = new Word(dataWidth);
            }
        }


        public void Run()
        {
            while (!halt)
            {
                TriggerClock();
            }
        }


        public void loadOperandAWord()
        {
            if (opAMode)
            {
                byte val = memory.GetByte(programCounter++);
                switch (val)
                {
                    case 0:
                        operandA = alu.o;
                        break;
                    case 1:
                        operandA = alu.a;
                        break;
                    case 2:
                        operandA = alu.b;
                        break;
                    case 3:
                        operandA = programCounter;
                        break;
                    case 4:
                        Word a = new Word(dataWidth);
                        a[0] = currentInstruction;
                        operandA = a;
                        break;
                    case 6:
                        Word b = new Word(dataWidth);
                        b[0] = operandB;
                        operandA = b;
                        break;
                    case > 63:
                        operandA = componentRegisters[val - 64];
                        break;
                    default:
                        break;
                }

            }
            else
            {
                operandA = new(dataWidth);
                for (int i = 0; i < dataWidth; i++)
                {
                    operandA[dataWidth - 1 - i] = memory.GetByte(programCounter + new Word(i, dataWidth));
                }
                programCounter += new Word(dataWidth, dataWidth);
            }
        }

        public void loadOperandAByte()
        {
            if (opAMode)
            {
                byte val = memory.GetByte(programCounter++);
                switch (val)
                {
                    case 0:
                        operandA[0] = alu.o[0];
                        break;
                    case 1:
                        operandA[0] = alu.a[0];
                        break;
                    case 2:
                        operandA[0] = alu.b[0];
                        break;
                    case 3:
                        operandA[0] = programCounter[0];
                        break;
                    case 4:
                        operandA[0] = currentInstruction;
                        break;
                    case 6:
                        operandA[0] = operandB;
                        break;
                    case > 63:
                        operandA[0] = componentRegisters[val - 64][0];
                        break;
                    default:
                        break;
                }

            }
            else
            {
                operandA[0] = memory.GetByte(programCounter++);
            }
        }

        public void loadOperandB()
        {
            if (opBMode)
            {
                byte val = memory.GetByte(programCounter++);
                switch (val)
                {
                    case 0:
                        operandB = alu.o[0];
                        break;
                    case 1:
                        operandB = alu.a[0];
                        break;
                    case 2:
                        operandB = alu.b[0];
                        break;
                    case 3:
                        operandB = programCounter[0];
                        break;
                    case 4:
                        operandB = currentInstruction;
                        break;
                    case 5:
                        operandB = operandA[0];
                        break;
                    case > 63:
                        operandB = componentRegisters[val - 64][0];
                        break;
                    default:
                        break;
                }

            }
            else
            {
                operandB = memory.GetByte(programCounter++);
            }
        }

        public void HLT()
        {
            halt = true;
        }
        public void NOP()
        {
            //nothing
        }
        public void ADD()
        {
            alu.ADD();
        }

        public void SUB()
        {
            alu.SUB();
        }

        public void LSL()
        {
            alu.LSL();
        }

        public void LSR()
        {
            alu.LSR();
        }

        public void NEG()
        {
            alu.NEG();
        }

        public void NOT()
        {
            alu.NOT();
        }

        public void CMP()
        {
            alu.CMP();
        }

        public void JMP()
        {
            loadOperandAWord();
            programCounter = operandA;
        }

        public void BFG()
        {
            loadOperandAWord();
            loadOperandB();
            bool flag = false;
            switch (operandB)
            {
                case 0:
                    flag = new BitArray(new byte[] { currentInstruction })[7];
                    break;
                case 1:
                    flag = new BitArray(new byte[] { currentInstruction })[6];
                    break;
                case 2:
                    flag = alu.eq;
                    break;
                case 3:
                    flag = alu.lt;
                    break;
                case 4:
                    flag = alu.gt;
                    break;
            }
            if (flag)
            {
                programCounter = operandA;
            }
        }

        public void BSR()
        {
            stack.Push(programCounter - Word.One(dataWidth));
            loadOperandAWord();
            programCounter = operandA;
        }

        public void RTN()
        {
            programCounter = stack.Pop;
        }

        public void RRB()
        {

        }

        public void RRW()
        {

        }

        public void RMB()
        {

        }

        public void RMW()
        {

        }

        public void WRB()
        {

        }

        public void WRW()
        {

        }

        public void WMB()
        {

        }

        public void WMW()
        {

        }

        public void RVB()
        {

        }

        public void RVW()
        {

        }

        public void SIN()
        {

        }

        public void INT()
        {

        }

        public void SFG()
        {

        }

        public void AND()
        {

        }

        public void LOR()
        {

        }

        public void XOR()
        {

        }

        public void TriggerClock()
        {
            currentInstruction = memory.GetByte(programCounter++);
            if (currentInstruction > 127)
            {
                opAMode = true;
                currentInstruction -= 128;
            }
            else
            {
                opAMode = false;
            }

            if (currentInstruction > 63)
            {
                opBMode = true;
                currentInstruction -= 64;
            }
            else
            {
                opBMode = false;
            }
            switch (currentInstruction)
            {
                case 0:
                    HLT();
                    break;
                case 2:
                    ADD();
                    break;
                case 3:
                    SUB();
                    break;
                case 4:
                    LSL();
                    break;
                case 5:
                    LSR();
                    break;
                case 6:
                    NEG();
                    break;
                case 7:
                    NOT();
                    break;
                case 8:
                    CMP();
                    break;
                case 9:
                    JMP();
                    break;
                case 10:
                    BFG();
                    break;
                case 14:
                    BSR();
                    break;
                case 15:
                    RTN();
                    break;
                case 16:
                    RRB();
                    break;
                case 17:
                    RRW();
                    break;
                case 18:
                    RMB();
                    break;
                case 19:
                    RMW();
                    break;
                case 20:
                    WRB();
                    break;
                case 21:
                    WRW();
                    break;
                case 22:
                    WMB();
                    break;
                case 23:
                    WMW();
                    break;
                case 24:
                    RVB();
                    break;
                case 25:
                    RVW();
                    break;
                case 26:
                    SIN();
                    break;
                case 27:
                    INT();
                    break;
                case 28:
                    SFG();
                    break;
                case 29:
                    AND();
                    break;
                case 30:
                    LOR();
                    break;
                case 31:
                    XOR();
                    break;
                default:
                    NOP();
                    break;
            }
        }
    }
}
