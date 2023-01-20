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
            Computer.dataWidth = dataWidth;
            memory = new RAM(data);
        }

        public static void Run()
        {
            while (!halted)
            {
                processor.Clock();
                Program.DumpInfo();
                Console.ReadLine();
            }
            Console.WriteLine("Computer halted.");
        }

        public static void Halt()
        {
            halted = true;
        }
    }
}