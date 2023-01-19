using RedFoxVM.Components;

namespace RedFoxVM
{
    internal static class Computer
    {
        private static bool halted = false;
        private static int dataWidth = 2;
        public static int DataWidth { get { return dataWidth; } }
        public static Processor processor = new Processor();
        public static RAM memory = new RAM(new byte[] {16, 4, 0});

        public static void Halt()
        {
            halted = true;
        }
    }
}