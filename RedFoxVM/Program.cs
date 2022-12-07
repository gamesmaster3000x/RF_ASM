using System.Collections;

namespace RedFoxVM {
    /// <summary>
    /// Entry point class for RF_ASM.
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            Computer computer = new(2, 1024, Array.Empty<byte>(), 32, 16, 256);
            Word a = new(new byte[] {10, 28, 30, 48});
            Console.WriteLine(a.ToBinaryString());
            Console.WriteLine(a);
            Console.WriteLine(new Word(new BitArray(a.ToByteArray())));
            Console.WriteLine(a.ToBinaryString());
            a = a << 4;
            Console.WriteLine(a.ToBinaryString());
            Console.WriteLine(a.ToHexString());
        }
    }
}