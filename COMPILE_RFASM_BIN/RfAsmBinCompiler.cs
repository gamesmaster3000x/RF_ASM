using COMPILE_RFASM_BIN;
using RF_ASM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        static void Main(string[] args)
        {
            Console.Write("Enter the name of the file to be compiled (including the file extension): ");
            string inputPath = Console.ReadLine();
            FileInfo inputFileInfo = new FileInfo(inputPath);
            if (!inputFileInfo.Exists)
            {
                throw new FileNotFoundException("Cannot file the file " + inputFileInfo.FullName);
            }

            // The raw byte data from the given file
            byte[] bytes = CompileFile(inputPath);

            // Dump
            Console.WriteLine("Dumping compiled bytes...");
            Utils.DisplayHexDump(bytes);

            // Write the bytes to the compiled file
            string outputPath = GetOutputFileName(inputPath);
            FileInfo outputFileInfo = new FileInfo(outputPath);
            Console.WriteLine("Storing compilation to " + outputFileInfo.FullName);
            File.WriteAllBytes(outputPath, bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        public static byte[] CompileFile(string inputPath)
        {
            Console.WriteLine("Compiling " + inputPath);
            string[] lines = File.ReadAllLines(inputPath);

            // Each line will contain no more than 3 bytes (instruction, register A, register B)
            List<byte> compiledBytes = new List<byte>();

            // For each line of the file, byteify and append to the compiled bytes
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Line number, e.g. 1/300
                Console.Write((i + 1) + "/" + lines.Length + " ");

                // The bytes, e.g. 01-04-a4
                byte[] lineBytes = ByteifyLine(line);
                foreach (byte b in lineBytes)
                {
                    Console.Write(b.ToString("X2") + " ");
                }

                // New line
                Console.WriteLine("");

                // Stick these bytes onto the end of the list of compiled bytes
                compiledBytes.AddRange(lineBytes);
            }
            Console.WriteLine("Done compiling " + inputPath);

            // Return as a byte[]
            return compiledBytes.ToArray();


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line">The input line, for example: LDA 0xaf</param>
        /// <returns>The bytes representing those commands, in the case of 'LDA 0xaf': 01 AF</returns>
        /// <exception cref="CompilationException"></exception>
        public static byte[] ByteifyLine(string line)
        {
            // Split "LDA 0xaf" into { "LDA", "0xaf" }
            string[] arguments = line.Split(" ");
            string instruction = arguments[0];

            // HLT
            if (instruction.Equals(Instructions.sHLT))
            {
                return InstructionBuilders.HLT(arguments);
            }
            // LDA
            else if (instruction.Equals(Instructions.sLDA))
            {
                return InstructionBuilders.LDA(arguments);
            }

            throw new CompilationException(line, "Invalid instruction");
        }
    }
}
