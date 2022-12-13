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
        public byte operandB;

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

        public void loadOperandAWord()
        {
            operandA = new(dataWidth);
            for (int i = 0; i < dataWidth; i++)
            {
                operandA[dataWidth - 1 - i] = memory.GetByte(programCounter + new Word(i, dataWidth));
            }
            programCounter += new Word(dataWidth, dataWidth);
        }

        public void loadOperandAByte()
        {
            operandA[0] = memory.GetByte(programCounter);
            programCounter++;
        }

        public void loadOperandB()
        {
            operandB = memory.GetByte(programCounter);
            programCounter++;
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
            currentInstruction = memory.GetByte(programCounter);
            programCounter++;
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
