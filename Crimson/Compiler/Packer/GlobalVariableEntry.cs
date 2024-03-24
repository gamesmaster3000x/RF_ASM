using System.Text.Json.Serialization;

namespace Compiler.Packer
{
    public class GlobalVariableEntry
    {
        [JsonPropertyName("size")] public int SizeBytes { get; set; }
    }
}
