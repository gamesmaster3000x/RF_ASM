using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM.Components
{
    internal class Terminal
    {
        private readonly byte lane;
        private readonly byte register;
        private readonly byte flagIn;
        private readonly byte flagOut;

        private Register dataRegister = new Register(Computer.DataWidth);

        public Terminal(byte lane)
        {
            this.lane = lane;
            register = (byte)(64 + lane);
            flagIn = (byte)(128 + lane);
            flagOut = (byte)(192 + lane);
        }
        
        public void Clock()
        {
            if (!Computer.processor.flags[flagIn])
            {
                return;
            }
            dataRegister.Word = Computer.processor.registers[register].Word;
            if (dataRegister.Word.Bytes[Computer.DataWidth - 1] == 0)
            {
                Console.Clear();
            }
            else if (dataRegister.Word.Bytes[Computer.DataWidth - 1] == 1)
            {
                Console.Write(Convert.ToChar(dataRegister.Byte));
            } 
            else if (dataRegister.Word.Bytes[Computer.DataWidth - 1] == 2)
            {
                for (int i = 1; i < Computer.DataWidth; i++)
                {
                    Console.Write(Convert.ToChar(dataRegister.Word.Bytes[Computer.DataWidth - i]));
                }
            }
        }
    }
}
