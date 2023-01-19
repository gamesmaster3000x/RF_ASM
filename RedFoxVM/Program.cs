using RedFoxVM.Components;

namespace RedFoxVM
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Created VM with " + Computer.memory.Capacity + " bytes of memory.");
            Console.WriteLine("Starting VM...");
            Computer.Run();
        }

        public static void DumpInfo()
        {
            Console.WriteLine("Selection register: " + Computer.processor.registers[7].Word.ToInt32());
            Console.WriteLine("Clock cycle started.");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("Register " + i + "'s current value is " + Computer.processor.registers[i].Word.ToInt32());
            }
        }
    }
}