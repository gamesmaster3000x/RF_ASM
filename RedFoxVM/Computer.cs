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
        public bool halt = false;
        public bool[] flags = new bool[256];

        public Memory memory;
        public Word[] generalRegisters;
        public Word[] componentRegisters;
        public Word[] interruptAddresses;
        public Stack stack;
        public ALU alu;


        public Word nextInstructionAddress;
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

            nextInstructionAddress = new Word(dataWidth);
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
            for (int i = 0; i < flags.Length; i++)
            {
                flags[i] = false;
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

        }

        public void CMP()
        {

        }

        public void JMP()
        {

        }

        public void BFG()
        {

        }

        public void BSR()
        {

        }

        public void RTN()
        {

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
            currentInstruction = 0;
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
