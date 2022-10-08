using RFASM_COMPILER.TOKEN_PARSER;
using System.Xml.Linq;

namespace RFASM_COMPILER.RFASM_BIN
{
    internal class RFASMCompilerMetadata : ITokenParserMetadata
    {

        private Dictionary<string, string> metadata = new Dictionary<string, string>();

        // Properties to make getting generic metadata fields easier
        // They're just fancy getters and setters for the values in the metadata dictionary
        private string _inputPath;
        private string _dataWidth;
        public string InputPath
        {
            get => metadata.ContainsKey("INPUT_PATH") ? metadata["INPUT_PATH"] : "";
            set => metadata["INPUT_PATH"] = value;
        }
        public int DataWidth
        {
            get => Int16.Parse(metadata.ContainsKey("DATA_WIDTH") ? metadata["DATA_WIDTH"] : "0");
            set => metadata["DATA_WIDTH"] = value.ToString();
        }

        // Constants
        public Dictionary<string, byte[]> constants = new Dictionary<string, byte[]>();


        public RFASMCompilerMetadata()
        {
        }

        public RFASMCompilerMetadata(string[] args)
        {
            for(int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-", StringComparison.Ordinal) && args.Length > i + 1)
                {
                    string key = arg.Substring(1);
                    string value = args[i + 1];
                    metadata[key] = value;
                    Console.WriteLine("Loaded argument: '" + key + "'='" + value + "'");
                    i++;
                } else
                {
                    Console.WriteLine("Did not understand argument " + arg);
                }
            }
        }

        public string GetValue(string key)
        {
            if (metadata.ContainsKey(key))
            {
                return metadata[key];
            }

            throw new NullReferenceException("Cannot get metadata key " + key + " because it's undefined!");
        }
    }
}