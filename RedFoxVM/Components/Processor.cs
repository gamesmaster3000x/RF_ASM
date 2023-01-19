namespace RedFoxVM.Components
{
    internal class Processor
    {
        public bool[] flags = new bool[256];
        public Register[] registers = new Register[256];
        public ALU alu = new ALU();
        
        public Processor()
        {
            for (int i = 0; i < 256; i++)
            {
                registers[i] = new Register(Computer.DataWidth);
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
                    //
                    break;
                case 9:
                    //
                    break;
                case 10:
                    //
                    break;
                case 11:
                    //
                    break;
                case 12:
                    //
                    break;
                case 13:
                    //
                    break;
                case 14:
                    //
                    break;
                case 15:
                    //
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
                case 32:
                    break;
                default:
                    break;
            }
        }

        private void loadOperandAWord()
        {
            byte[] bytes = new byte[Computer.DataWidth];
            for (int i = 0; i < Computer.DataWidth; i++)
            {
                bytes[Computer.DataWidth - (i + 1)] = Computer.memory.GetByte(registers[3].Word++);
            }
            registers[5].Word = new Word(bytes);
        }

        private void loadOperandAByte()
        {
            registers[5].Byte = Computer.memory.GetByte(registers[3].Word++);
        }

        private void loadOperandB()
        {
            registers[6].Byte = Computer.memory.GetByte(registers[3].Word++);
        }

        private void ReadRegisterByte()
        {
            loadOperandAByte();
            loadOperandB();
            registers[7].Byte = registers[5].Word.Bytes[registers[6].Byte];
        }

        private void ReadRegisterWord()
        {
            loadOperandAByte();
            registers[7].Word = registers[5].Word;
        }

        private void ReadMemoryByte()
        {
            loadOperandAWord();
            
        }
    }
}
