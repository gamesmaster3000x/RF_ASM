using RedFoxAssembly.CSharp.Statements;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace RedFoxAssembly.CSharp.Core
{
    internal class ProgramMetadata: ICommand
    {
        public static readonly string DATE_FORMAT = "dd/MM/yyyy,HH:mm:ss"; 
        public static readonly string WATERMARK = "RFASM Compiler by GenElectrovise";
        public static readonly string AUTHORS = "Authors:";
        public static readonly string DATE_TIME = "DateTime:";
        public static readonly string CONSTANTS = "Constants:";
        public static readonly string LABELS = "Labels:";

        public List<string> Authors { get; protected set; }
        public Dictionary<string, int> Labels { get; protected set; }
        public Dictionary<string, string> Constants { get; protected set; }

        public ProgramMetadata () : this(new List<string>(), new Dictionary<string, int>(), new Dictionary<string, string>()) {}

        public ProgramMetadata (List<string> authors, Dictionary<string, int> labels, Dictionary<string, string> constants)
        {
            Authors = authors;
            Labels = labels;
            Constants = constants;
        }

        public bool AddAuthors { get; set; } = true;
        public bool AddDateTime { get; set; } = true;
        public bool AddConstants { get; set; } = true;
        public bool AddLabels { get; set; } = true;

        public int GetPredictedLength (RFASMCompiler compiler)
        {
            return GetBytes(compiler).Length;
        }

        private List<byte> GetWatermarkBytes (int wORD)
        {
            return Encoding.ASCII.GetBytes(WATERMARK).ToList();
        }

        private List<byte> GetAuthorBytes (int dataWidth)
        {
            List<byte> bytes = new List<byte>(); 

            if (AddAuthors)
            {
                bytes.AddRange(Encoding.ASCII.GetBytes(AUTHORS));

                // Word describing how long the authors list is
                // Authors list
                string sAuthors = String.Join(',', Authors);
                bytes.AddRange(Encoding.ASCII.GetBytes(sAuthors));
            }
            bytes.InsertRange(0, CompilerUtils.IntToBytesAtWidth(dataWidth, bytes.Count));

            return bytes;
        }

        private List<byte> GetDateTimeBytes (int dataWidth)
        {
            List<byte> bytes = new List<byte>();

            if (AddDateTime)
            {
                bytes.AddRange(Encoding.ASCII.GetBytes(DATE_TIME));

                string sDateTime = DateTime.UtcNow.ToString(DATE_FORMAT);
                bytes.AddRange(Encoding.ASCII.GetBytes(sDateTime));
            }
            bytes.InsertRange(0, CompilerUtils.IntToBytesAtWidth(dataWidth, bytes.Count));

            return bytes;
        }

        private List<byte> GetConstants (RFASMCompiler compiler, int dataWidth)
        {
            List<byte> bytes = new List<byte>();

            if (AddConstants)
            {
                bytes.AddRange(Encoding.ASCII.GetBytes(CONSTANTS));

                // Word describing how long the constants table is
                // Constants table
                foreach (KeyValuePair<string, IData> pair in compiler.Constants)
                {
                    byte[] keyBytes = Encoding.ASCII.GetBytes(pair.Key);
                    byte[] valBytes = pair.Value.GetBytes(compiler);

                    byte lengthByte = (byte) keyBytes.Length;
                    if (pair.Value is Word) lengthByte |= 0b10000000;
                    bytes.Add(lengthByte);

                    bytes.AddRange(keyBytes);
                    bytes.AddRange(valBytes);
                }
            }
            bytes.InsertRange(0, CompilerUtils.IntToBytesAtWidth(dataWidth, bytes.Count));

            return bytes;
        }

        private List<byte> GetLabels (RFASMCompiler compiler, int dataWidth)
        {
            List<byte> bytes = new List<byte>();

            if (AddLabels)
            {
                bytes.AddRange(Encoding.ASCII.GetBytes(LABELS));

                // Word describing how long the labels table is
                // Labels table
                foreach (KeyValuePair<string, LabelCommand> pair in compiler.Labels)
                {
                    byte[] keyBytes = Encoding.ASCII.GetBytes(pair.Key);
                    byte[] valBytes = pair.Value.GetBytes(compiler);

                    byte lengthByte = (byte) keyBytes.Length;
                    bytes.Add(lengthByte);

                    bytes.AddRange(keyBytes);
                    bytes.AddRange(valBytes);
                }
            }
            bytes.InsertRange(0, CompilerUtils.IntToBytesAtWidth(dataWidth, bytes.Count));

            return bytes;
        }

        private byte GetFlagByte ()
        {
            byte b = 0b00000000;
            if (AddAuthors) b |= 0b10000000;
            if (AddDateTime) b |= 0b01000000;
            if (AddConstants) b |= 0b00100000;
            if (AddLabels) b |= 0b00010000;
            return b;
        }

        public byte[] GetBytes (RFASMCompiler compiler)
        {
            int WORD = compiler.args!.DataWidth;

            List<byte> bWatermark = GetWatermarkBytes(WORD);
            List<byte> bAuthors = GetAuthorBytes(WORD);
            List<byte> bDateTime = GetDateTimeBytes(WORD);
            List<byte> bConstants = GetConstants(compiler, WORD);
            List<byte> bLabels = GetLabels(compiler, WORD);

            List<byte> start = new List<byte> { GetFlagByte() };

            byte[] result = start.Concat(bWatermark).Concat(bAuthors).Concat(bDateTime).Concat(bConstants).Concat(bLabels).ToArray();
            return result;
        }
    }
}
