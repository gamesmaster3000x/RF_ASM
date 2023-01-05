using RedFoxVM.Components;

namespace RedFoxVM
{
    static class Program
    {
        static void Main(string[] args)
        {
            Register r = new Register(2);
            foreach (byte b in r.Word.Bytes)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine();
            Console.WriteLine(r.Byte);
            r.Byte = 234;
            foreach (byte b in r.Word.Bytes)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine();
            Console.WriteLine(r.Byte);
        }
    }
}