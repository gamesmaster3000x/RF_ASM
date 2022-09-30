namespace RF_ASM
{
    internal class Utils
    {

        public static byte[] GetDataFromFile(string path)
        {
            BinaryReader reader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate));
            byte[] data = new byte[reader.BaseStream.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = reader.ReadByte();
            }
            reader.Close();
            return data;
        }

        public static void DisplayHexDump(byte[] data)
        {
            int lines = (data.Length + 15) / 16;
            string linesHex = (lines - 1).ToString("X");
            Console.WriteLine(new string(' ', linesHex.Length) + " 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F");
            for (int i = 0; i < lines; i++)
            {
                Console.Write(i.ToString("X" + linesHex.Length.ToString()));
                for (int j = 0; j < 16; j++)
                {
                    if (i * 16 + j < data.Length)
                    {
                        Console.Write(" " + data[i * 16 + j].ToString("X2"));
                    }
                }
                Console.WriteLine();
            }
        }
    }
}