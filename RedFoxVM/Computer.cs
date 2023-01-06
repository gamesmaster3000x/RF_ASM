using RedFoxVM.Components;

namespace RedFoxVM
{
    internal static class Computer
    {
        private static int dataWidth = 2;
        public static int DataWidth { get { return dataWidth; } }
        public static Processor processor = new Processor();
        public static RAM memory = new RAM();
    }
}