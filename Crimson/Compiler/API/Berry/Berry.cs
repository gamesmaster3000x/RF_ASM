using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Compiler.API.Berry
{
    public class Berry
    {
        public static readonly Encoding ENCODING = Encoding.Unicode;

        [JsonPropertyName("datawidth")] public int DataWidth { get; set; }
        [JsonPropertyName("target")] public string Target { get; set; }
        [JsonPropertyName("links")] public IList<string> Links { get; set; }
        [JsonPropertyName("functions")] public Dictionary<string, FunctionEntry> Functions { get; set; }
        [JsonPropertyName("global_variables")] public Dictionary<string, GlobalVariableEntry> GlobalVariables { get; set; }
    }
}
