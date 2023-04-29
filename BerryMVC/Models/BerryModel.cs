namespace BerryMVC.Models
{
    public class BerryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public byte[] ContentsB64 { get; set; }
    }
}
