class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the name of the file to be read (including the file extension): ");
        string s = Console.ReadLine();
        BinaryReader reader = new BinaryReader(File.Open("resources/" + s, FileMode.OpenOrCreate));
        Console.WriteLine(Convert.ToHexString(new byte[]{reader.ReadByte()}));
        reader.Close();
    }
}