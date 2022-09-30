namespace RF_ASM {
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the name of the file to be read (including the file extension): ");
            byte[] data = Utils.GetDataFromFile("resources/" + Console.ReadLine());
            Utils.DisplayHexDump(data);
        }
    }
}