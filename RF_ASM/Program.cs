namespace RedFoxVM {
    
    /// <summary>
    /// Entry point class for RF_ASM.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the name of the file to be read (including the file extension): ");        
            
            // The raw byte data from the given file
            byte[] data = Utils.GetDataFromFile("resources/" + Console.ReadLine());
            Utils.DisplayHexDump(data);


        }
    }
}
