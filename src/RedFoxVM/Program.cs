using RedFoxVM.Components;
using System.Text;

namespace RedFoxVM
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the datawidth of the desired VM in bytes:");
            int dw = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the name of the file to load:");
            BinaryReader reader = new BinaryReader(File.Open(Console.ReadLine(),FileMode.Open), Encoding.ASCII);
            List<byte> bytes = new List<byte>();
            while (reader.PeekChar() != -1)
            {
                bytes.Add(reader.ReadByte());
            }
            reader.Close();
            Computer.Initialise(dw, bytes.ToArray());
            Console.WriteLine("Created VM with " + Computer.memory.Capacity + " bytes of memory.");
            Console.WriteLine("Starting VM...");
            DateTime start = DateTime.Now;
            int cycles = Computer.Run();
            TimeSpan runtime = DateTime.Now - start;
            Console.WriteLine("The VM completed " + cycles + " cycles in " + runtime + " resulting in an average of " + (runtime / cycles).TotalMilliseconds + "ms per cycle.");
        }

        public static void DumpInfo()
        {
            Console.WriteLine("Clock cycle started.");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("Register " + i + "'s current value is " + Computer.processor.registers[i].Word.ToInt32());
            }
        }
    }
}