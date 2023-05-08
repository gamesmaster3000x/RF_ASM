using System.Text.Json;
using System.Text.Json.Serialization;

namespace Compiler.Packer
{
    [JsonConverter(typeof(Converter))]
    public class FunctionEntry
    {

        public byte[] MachineCode { get; set; } = new byte[0];
        public string Source { get; set; } = "";
        public string Links { get; set; } = "";

        public FunctionEntry () { }

        public FunctionEntry (byte[] machineCode, string source, string links)
        {
            MachineCode = machineCode;
            Source = source;
            Links = links;
        }

        /// <summary>
        /// Custom 
        /// </summary>
        public class Converter : JsonConverter<FunctionEntry>
        {
            public override FunctionEntry? Read (ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException("Berry function entries must be JSON objects. Found: " + reader.TokenType + " at " + reader.Position);

                string mcb64 = GetJsonString("mcb64", reader);
                string srcb64 = GetJsonString("srcb64", reader);
                string links = GetJsonString("links", reader);

                byte[] mc = Convert.FromBase64String(mcb64);
                byte[] srcBytes = Convert.FromBase64String(srcb64);
                string src = Berry.ENCODING.GetString(srcBytes);

                return new FunctionEntry(mc, src, links);
            }

            private string GetJsonString (string key, Utf8JsonReader reader)
            {
                // Key
                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException(
                        $"Illegal Berry function entry syntax. " +
                        $"Expected property name, got '{reader.TokenType}' at position {reader.Position}");

                string readKey = reader.GetString()!;
                if (!key.Equals(readKey))
                    throw new JsonException(
                        $"Illegal Berry function entry syntax. " +
                        $"Expected property '{key}', got '{readKey}' at position {reader.Position}");

                // Value
                if (!reader.Read())
                    throw new JsonException($"Reader expected a value, but was unable to read past position {reader.Position}.");

                if (reader.TokenType != JsonTokenType.String)
                    throw new JsonException(
                       $"Illegal Berry function entry syntax. " +
                       $"Expected property '{key}', got '{readKey}' at position {reader.Position}");

                string value = reader.GetString()!;
                return value;
            }

            public override void Write (Utf8JsonWriter writer, FunctionEntry value, JsonSerializerOptions options)
            {
                string mcb64 = Convert.ToBase64String(value.MachineCode);
                byte[] srcBytes = Berry.ENCODING.GetBytes(value.Source);
                string srcb64 = Convert.ToBase64String(srcBytes);

                writer.WriteString("mcb64", mcb64);
                writer.WriteString("srcb64", srcb64);
                writer.WriteString("links", value.Links);
            }
        }
    }
}
