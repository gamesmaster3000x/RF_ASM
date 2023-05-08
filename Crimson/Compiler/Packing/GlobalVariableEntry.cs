using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Compiler.Packing
{
    [JsonConverter(typeof(Converter))]
    public class GlobalVariableEntry
    {
        [JsonPropertyName("size")] public int SizeBytes { get; set; }
    }
}
