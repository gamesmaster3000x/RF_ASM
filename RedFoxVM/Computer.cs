using RedFoxVM.Components;

namespace RedFoxVM
{
    internal static class Computer
    {
        private static bool halted = false;
        private static int dataWidth = 1;
        public static int DataWidth { get { return dataWidth; } }
        public static Processor processor = new Processor();
        public static RAM memory;

        public static void Initialise(int dataWidth, byte[] data)
        {
            Console.WriteLine("Started creating VM...");
            Computer.dataWidth = dataWidth;
            memory = new RAM(data);
            Console.WriteLine("Successfully created VM with " + memory.Capacity + " bytes of memory.");
        }
        
        public static int Run()
        {
            int cycles = 0;
            while (!halted)
            {
                processor.Clock();
                cycles++;
            }
            Program.DumpInfo();
            Console.WriteLine("Computer halted.");
            return cycles;
        }

        public static void Halt()
        {
            halted = true;
        }
    }
}