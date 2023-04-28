using System.Text;

namespace Berry.DB
{
    /// <summary>
    /// 
    /// </summary>
    public class BerryEntry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public byte[] ContentsB64 { get; set; }

        public static void ToB64 (byte[] unicodeBytes, char[] b64Bytes)
        {
            _ = Convert.ToBase64CharArray(
                unicodeBytes,
                0,
                unicodeBytes.Length,
                b64Bytes,
                0
                );
        }
    }
}
