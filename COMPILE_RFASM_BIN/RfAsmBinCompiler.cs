using COMPILE_RFASM_BIN;
using RF_ASM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COMPILE_RF_ASM_BIN
{
    /// <summary>
    /// Compiler for converting a list of RF ASM instructions into binary for interpretation. 
    /// Each instruction (for example, "LDA 0xaf") should be on a new line, and arguments are seperated by a space.
    /// </summary>
    class RfAsmBinCompiler
    {
        public const int DATA_WIDTH = 2;

        private static Dictionary<string, byte[]> labels = new Dictionary<string, byte[]>();
        private static Dictionary<string, byte[]> constants = new Dictionary<string, byte[]>();

        static void Main(string[] args)
        {
            string inputPath = GetInputFilePath();

            Metadata meta = new Metadata();
            meta.INPUT_PATH = inputPath;
            meta.DATA_WIDTH = 1;

            // The raw byte data from the given file
            byte[] bytes = CompileFile(meta);

            // Dump
            Console.WriteLine("Dumping compiled bytes...");
            Utils.DisplayHexDump(bytes);

            // Write the bytes to the compiled file
            WriteCompilation(inputPath, bytes);
        }

        private static void WriteCompilation(string inputPath, byte[] bytes)
        {
            string outputPath = GetOutputFileName(inputPath);
            FileInfo outputFileInfo = new FileInfo(outputPath);
            Console.WriteLine("Storing compilation to " + outputFileInfo.FullName);
            BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.OpenOrCreate));
            writer.Write(bytes);
            writer.Flush();
            writer.Close();
        }

        private static string GetInputFilePath()
        {
            Console.Write("Enter the name of the file to be compiled (including the file extension): ");
            string inputPath = Console.ReadLine();
            FileInfo inputFileInfo = new FileInfo(inputPath);
            if (!inputFileInfo.Exists)
            {
                throw new FileNotFoundException("Cannot file the file " + inputFileInfo.FullName);
            }

            return inputPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        public static byte[] CompileFile(Metadata meta)
        {
            Console.WriteLine("Compiling " + meta.INPUT_PATH);
            string[] rawLinesArr = File.ReadAllLines(meta.INPUT_PATH);

            List<byte> compiledBytes = new List<byte>();
            List<string> rawLines = rawLinesArr.ToList();

            //
            //FindConstants(rawLines);
            //List<string> parsedLines = ReplaceConstants(rawLines, compiledBytes);
            List<string> parsedLines = new Parser(meta).Parse(rawLines);

            // For each line of the file, byteify and append to the compiled bytes
            Byteify(parsedLines, compiledBytes);

            Console.WriteLine("Done compiling " + meta.INPUT_PATH);

            // Return as a byte[]
            return compiledBytes.ToArray();


        }

        /// <summary>
        /// Passes through all of the lines and converts the instructions to bytes.
        /// </summary>
        /// <param name="parsedLines"></param>
        /// <param name="compiledBytes"></param>
        private static void Byteify(List<string> parsedLines, List<byte> compiledBytes)
        {
            for (int i = 0; i < parsedLines.Count; i++)
            {
                string line = parsedLines[i];

                // Line number, e.g. 1/300
                Console.Write((i + 1) + "/" + parsedLines.Count + " ");

                // The bytes, e.g. 01-04-a4
                byte[] lineBytes = Bytifier.ByteifyLine(line);
                foreach (byte b in lineBytes)
                {
                    Console.Write(b.ToString("X2") + " ");
                }

                // New line
                Console.WriteLine("");

                // Stick these bytes onto the end of the list of compiled bytes
                compiledBytes.AddRange(lineBytes);
            }
        }

        /// <summary>
        /// Generates the name of the output file based on the input file. For example Program.rfx will become Program.rfbin.
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <returns></returns>
        public static string GetOutputFileName(string inputFileName)
        {
            // The input file extension and output file extension
            string RF_BIN_FileEnding = "rfb";

            // Fancy regular expression stuff to replace the .input with the .output
            // () are a class
            // Searches for the final . , then takes everything after it and replaces with the new file ending
            return Regex.Replace(inputFileName, "([^.]+$)", RF_BIN_FileEnding); 
        }
    }
}
