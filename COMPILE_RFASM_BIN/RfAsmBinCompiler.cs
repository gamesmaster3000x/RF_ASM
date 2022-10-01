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
            BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.OpenOrCreate));
            writer.Write(bytes);
            writer.Flush();
            writer.Close();
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
            string[] cleanArgs = GenerateCleanArguments(line);
            string instruction = cleanArgs[0];

            // Protect against weird exceptions (can only throw CompilationException)
            Instructions inst;
            try
            {
                if(!Enum.TryParse(instruction, true, out inst))
                {
                    throw new CompilationException(line, "Could not parse instruction (" + instruction + ")");
                }
            } catch (Exception e)
            {
                throw new CompilationException(line, "Illegal arguments for parsing instruction " + instruction + " (" + e + ")");
            }

            byte b = (byte) inst;

            // Switch the instruction
            switch (inst)
            {
                // Halt
                case Instructions.HLT:
                    RequireLength(inst, cleanArgs, 1);
                    return new byte[] { b };
                // Load to A reg
                case Instructions.LDA:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // Load to B reg
                case Instructions.LDB:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // Store A + B in C
                case Instructions.ADD:
                    RequireLength(inst, cleanArgs, 1);
                    return new byte[] { b };
                // Store A - B in C
                case Instructions.SUB:
                    RequireLength(inst, cleanArgs, 1);
                    return new byte[] { b };
                // Compare value at α to β
                case Instructions.CMP:
                    RequireLength(inst, cleanArgs, 3);
                    return new byte[] { b, ToByte(cleanArgs[1]), ToByte(cleanArgs[2]) };
                // Jump to the memory address stored at address α
                case Instructions.B:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // If CMP returns equal, jump to address α
                case Instructions.BEQ:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // If CMP returns not-equal, jump to address α
                case Instructions.BNE:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // If CMP returns greater-than, jump to address α
                case Instructions.BGT:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // Push next memory address to stack and jump to address α
                case Instructions.BSR:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // Pop from stack and jump there
                case Instructions.RTN:
                    RequireLength(inst, cleanArgs, 1);
                    return new byte[] { b };
                // Push value at address β into register α
                case Instructions.LDR:
                    RequireLength(inst, cleanArgs, 3);
                    return new byte[] { b, ToByte(cleanArgs[1]), ToByte(cleanArgs[2]) };
                // Move value of register C to α
                case Instructions.CTM:
                    RequireLength(inst, cleanArgs, 2);
                    return new byte[] { b, ToByte(cleanArgs[1]) };
                // Move value from register α to address β
                case Instructions.RTM:
                    RequireLength(inst, cleanArgs, 3);
                    return new byte[] { b, ToByte(cleanArgs[1]), ToByte(cleanArgs[2]) };

            }

            //
            throw new CompilationException(line, "Could not find definition for ");
        }

        public static void RequireLength(Instructions instruction, string[] arr, int requiredLength)
        {
            if (arr.Length != requiredLength)
            {
                throw new CompilationException(instruction + " requires at least " + requiredLength + " arguments (including the instruction itself)");
            }
        }

        public static byte ToByte(string instruction)
        {
            if (instruction.StartsWith("0x"))
            {
                instruction = instruction.Substring(2);
            }
            byte output = Byte.Parse(instruction, System.Globalization.NumberStyles.HexNumber);
            return output;
        }

        /// <summary>
        /// Removes nasty things like comments from the arguments
        /// </summary>
        /// <param name="rawArgs"></param>
        /// <returns></returns>
        public static string[] GenerateCleanArguments(string rawLine)
        {
            // Split "LDA 0xaf" into { "LDA", "0xaf" }
            // Split based on the phrase (\b): any number of spaces, followed by any character.
            string[] rawArgs = Regex.Split(rawLine, " +(?=.)");

            for (int i = 0; i < rawArgs.Length; i++)
            {
                string arg = rawArgs[i];

                if (arg.StartsWith("//", StringComparison.Ordinal))
                {

                    // LDA 0x00 // Something here 
                    // 5 long
                    // // at index 2
                    // So starting at index 0, take 2 (indices 0 and 1)

                    return rawArgs.Take(i).ToArray();
                }
            }

            // Guess they're clean
            return rawArgs;
        }
    }
}
