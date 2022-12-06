namespace RedFoxVM {
    /// <summary>
    /// Entry point class for RF_ASM.
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            Computer computer = new(2, 1024, new byte[8], 32, 16, 256);
            Value a = new(new byte[] { 10, 0 });
            Value b = new(new byte[] { 255, 0 });
            Value c = a + b;
            Value d = -c;
            Console.WriteLine(a.ToBinaryString);
            Console.WriteLine(b.ToBinaryString);
            Console.WriteLine(c.ToBinaryString);
            Console.WriteLine(d.ToBinaryString);
        }
    }
}