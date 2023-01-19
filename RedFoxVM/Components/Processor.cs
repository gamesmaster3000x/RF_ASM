namespace RedFoxVM.Components
{
    internal class Processor
    {
        public bool[] flags = new bool[256];
        public Register[] registers = new Register[256];
        public Register[] interrupts = new Register[256];
        public ALU alu = new ALU();
        
        public Processor()
        {
            for (int i = 0; i < 256; i++)
            {
                registers[i] = new Register(Computer.DataWidth);
                interrupts[i] = new Register(Computer.DataWidth);
                flags[i] = false;
            }
        }

        public void Clock()
        {
            Fetch();
            Decode();
            Execute();
        }

        private void Fetch()
        {
            registers[4].Byte = Computer.memory.GetByte(registers[3].Word);
            registers[3].Word++;
        }

        private void Decode()
        {
            if (registers[4].Byte >= 128)
            {
                registers[4].Byte -= 128;
                flags[0] = true;
            }
            else
            {
                flags[0] = false;
            }

            if (registers[4].Byte >= 64)
            {
                registers[4].Byte -= 64;
                flags[1] = true;
            }
            else
            {
                flags[1] = false;
            }
        }

        private void Execute()
        {
            switch (registers[4].Byte)
            {
                case 0:
                    Computer.Halt();
                    break;
                case 1:
                    break;
                case 2:
                    alu.Add();
                    break;
                case 3:
                    alu.Subtract();
                    break;
                case 4:
                    alu.ShiftLeft();
                    break;
                case 5:
                    alu.ShiftRight();
                    break;
                case 6:
                    alu.Negate();
                    break;
                case 7:
                    alu.Not();
                    break;
                case 8:
                    //CMP
                    break;
                case 9:
                    //JMP
                    break;
                case 10:
                    //BFG
                    break;
                case 11:
                    //NA
                    break;
                case 12:
                    //NA
                    break;
                case 13:
                    //NA
                    break;
                case 14:
                    //BSR
                    break;
                case 15:
                    //RTN
                    break;
                case 16:
                    ReadRegisterByte();
                    break;
                case 17:
                    ReadRegisterWord();
                    break;
                case 18:
                    ReadMemoryByte();
                    break;
                case 19:
                    ReadMemoryWord();
                    break;
                case 20:
                    WriteRegisterByte();
                    break;
                case 21:
                    WriteRegisterWord();
                    break;
                case 22:
                    WriteMemoryByte();
                    break;
                case 23:
                    WriteMemoryWord();
                    break;
                case 24:
                    ReadValueByte();
                    break;
                case 25:
                    ReadValueWord();
                    break;
                case 26:
                    SetInterrupt();
                    break;
                case 27:
                    //INT
                    break;
                case 28:
                    SetFlag();
                    break;
                case 29:
                    alu.And();
                    break;
                case 30:
                    alu.Or();
                    break;
                case 31:
                    alu.Xor();
                    break;
                default:
                    break;
            }
        }

        private void loadOperandAWord()
        {
            if (flags[0])
            {
                registers[5].Word = registers[Computer.memory.GetByte(registers[3].Word)].Word;
                registers[3].Word++;
            }
            else
            {
                byte[] bytes = new byte[Computer.DataWidth];
                for (int i = 0; i < Computer.DataWidth; i++)
                {
                    bytes[Computer.DataWidth - (i + 1)] = Computer.memory.GetByte(registers[3].Word);
                    registers[3].Word++;
                }
                registers[5].Word = new Word(bytes);
            }
        }

        private void loadOperandAByte()
        {
            if (flags[0])
            {
                registers[5].Byte = registers[Computer.memory.GetByte(registers[3].Word)].Byte;
                registers[3].Word++;
            }
            else
            {
                /**/registers[5].Byte = Computer.memory.GetByte(registers[3].Word);
                registers[3].Word++;
            }
        }

        private void loadOperandB()
        {
            if (flags[1])
            {
                registers[6].Byte = registers[Computer.memory.GetByte(registers[3].Word)].Byte;
                registers[3].Word++;
            }
            else
            {
                registers[6].Byte = Computer.memory.GetByte(registers[3].Word);
                registers[3].Word++;
            }
        }

        private void ReadRegisterByte()
        {
            loadOperandAByte();
            loadOperandB();
            registers[7].Byte = registers[registers[5].Byte].Word.Bytes[registers[6].Byte];
        }

        private void ReadRegisterWord()
        {
            loadOperandAByte();
            registers[7].Word = registers[registers[5].Byte].Word;
        }

        private void ReadMemoryByte()
        {
            loadOperandAWord();
            registers[7].Byte = Computer.memory.GetByte(registers[5].Word);
        }

        private void ReadMemoryWord()
        {
            loadOperandAWord();
            byte[] bytes = new byte[Computer.DataWidth];
            for (int i = 0; i < Computer.DataWidth; i++)
            {
                bytes[Computer.DataWidth - (i + 1)] = Computer.memory.GetByte(registers[5].Word);
                registers[5].Word++;
            }
            registers[7].Word = new Word(bytes);
        }

        private void WriteRegisterByte()
        {
            loadOperandAByte();
            loadOperandB();
            byte[] bytes = registers[registers[5].Byte].Word.Bytes;
            bytes[registers[6].Byte] = registers[7].Byte;
            registers[registers[5].Byte].Word = new Word(bytes);
        }

        private void WriteRegisterWord()
        {
            loadOperandAByte();
            registers[registers[5].Byte].Word = registers[7].Word;
        }

        private void WriteMemoryByte()
        {
            loadOperandAWord();
            Computer.memory.SetByte(registers[5].Word, registers[7].Byte);
        }

        private void WriteMemoryWord()
        {
            loadOperandAWord();
            for (int i = 0; i < Computer.DataWidth; i++)
            {
                Computer.memory.SetByte(registers[5].Word, registers[7].Word.Bytes[Computer.DataWidth - (i + 1)]);
                registers[5].Word++;
            }
        }

        private void ReadValueByte()
        {
            loadOperandAByte();
            registers[7].Byte = registers[5].Byte;
        }

        private void ReadValueWord()
        {
            loadOperandAWord();
            registers[7].Word = registers[5].Word;
        }

        private void SetInterrupt()
        {
            loadOperandAWord();
            loadOperandB();
            interrupts[registers[6].Byte].Word = registers[5].Word;
        }

        private void SetFlag()
        {
            loadOperandAByte();
            loadOperandB();
            flags[registers[5].Byte] = registers[6].Word[0];
        }
    }
}
