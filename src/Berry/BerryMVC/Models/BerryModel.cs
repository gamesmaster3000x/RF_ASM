using System;

namespace BerryMVC.Models
{
    public class BerryModel
    {
        public int ID { get; set; }
        public string VendorName { get; set; }
        public string ArtifactName { get; set; }
        public string Version { get; set; }
        public byte[] ContentsB64 { get; set; }

        public string FullName { get => $"{VendorName}:{ArtifactName}@{Version}"; }


        public static string FormatFullName (string vendor, string artifact, string version)
        {
            return new BerryModel()
            {
                VendorName = vendor,
                ArtifactName = artifact,
                Version = version
            }.FullName;
        }
    }
}
