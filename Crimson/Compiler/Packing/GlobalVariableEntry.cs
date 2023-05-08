using System.Text.Json.Serialization;

namespace Compiler.Packing
{
    public class GlobalVariableEntry
    {
        [JsonPropertyName("size")] public int SizeBytes { get; set; }
    }
}
