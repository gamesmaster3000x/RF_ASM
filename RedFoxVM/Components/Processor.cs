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
    }
}
