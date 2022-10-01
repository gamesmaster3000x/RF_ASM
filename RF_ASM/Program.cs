namespace RF_ASM {
    
    /// <summary>
    /// Entry point class for RF_ASM.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the name of the file to be read (including the file extension): ");        
            string filePath = Console.ReadLine();
            
            // The raw byte data from the given file
            byte[] data = Utils.GetDataFromFile("resources/" + filePath);
            Utils.DisplayHexDump(data);
        }
    }
}
