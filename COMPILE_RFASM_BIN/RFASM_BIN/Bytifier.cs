using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN
{
    internal static class Bytifier
    {

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
                if (!Enum.TryParse(instruction, true, out inst))
                {
                    throw new CompilationException(line, "Could not parse instruction (" + instruction + ")");
                }
            }
            catch (Exception e)
            {
                throw new CompilationException(line, "Illegal arguments for parsing instruction " + instruction + " (" + e + ")");
            }

            byte b = (byte)inst;

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
            byte output = byte.Parse(instruction, System.Globalization.NumberStyles.HexNumber);
            return output;
        }
    }
}