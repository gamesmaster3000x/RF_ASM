using RedFoxVM.Components;

namespace RedFoxVM
{
    internal static class Computer
    {
        private static bool halted = false;
        private static int dataWidth = 2;
        public static int DataWidth { get { return dataWidth; } }
        public static Processor processor = new Processor();
        public static RAM memory = new RAM(new byte[] {25, 10, 132, 21, 1, 25, 4, 52, 21, 2, 3, 17, 0});

        public static void Run()
        {
            while (!halted)
            {
                Program.DumpInfo();
                processor.Clock();
            }
            Console.WriteLine("Computer halted.");
        }

        public static void Halt()
        {
            halted = true;
        }
    }
}