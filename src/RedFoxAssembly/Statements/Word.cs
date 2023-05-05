using NLog.Targets;
using RedFoxAssembly.Core;
using RedFoxAssembly.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RedFoxAssembly.Statements.IData;

namespace RedFoxAssembly.Statements
{
    internal class Word : IData
    {
        public bool TargetingRegister { get; protected set; }
        public bool TargetingLabel { get; protected set; }
        public byte[]? Data { get; protected set; }
        public string? Identifier { get; protected set; }

        public Word (bool isTargetingRegister, byte[] data)
        {
            TargetingRegister = isTargetingRegister;

            if (data == null | data.Length < 1) throw new ParsingException("Cannot assign fewer than 1 byte to a word");
            Data = data;
        }

        public Word (bool isTargetingRegister, string identifier)
        {
            TargetingRegister = isTargetingRegister;

            if (string.IsNullOrWhiteSpace(identifier)) throw new ParsingException("Cannot assign a null or whitespace value-mapping to a word");
            Identifier = identifier;
        }

        public byte[] GetBytes (RFASMCompiler compiler)
        {
            // Return the value of the named constant 
            if (Identifier != null)
            {
                if (compiler.Constants.TryGetValue(Identifier, out IData? val))
                {
                    byte[] v = val.GetBytes(compiler);
                    if (v.Length != compiler.Options!.DataWidth)
                        throw new CompilationException($"Constant {Identifier} with value [{string.Join(',', v)}] is not of correct width {compiler.Options.DataWidth} for word.");
                    return v;
                }
                else
                {
                    throw new CompilationException("(Word) Cannot get byte value of undeclared constant value " + val);
                }
            }

            // Return _data
            if (Data != null)
            {

                // Is targeting register, so return register address (1 byte only)
                if (IsTargetingRegister())
                {
                    if (Data.Length != 1)
                        throw new CompilationException($"Word [{string.Join(',', Data)}] targetting register is length {Data.Length}, but it should have length 1.");
                    else
                        return Data;
                }

                // Not targeting register, so return whole value (must be of correct data width)
                else
                {
                    // Illegal data width
                    if (Data.Length != compiler.Options!.DataWidth)
                    {
                        throw new CompilationException($"Width {Data.Length} of word [{string.Join(',', Data)}] does not match defined data width {compiler.Options.DataWidth}.");
                    }
                    // If contains no data
                    else if (Data.Length < 1)
                    {
                        throw new CompilationException($"Word [{string.Join(',', Data)}] width {Data.Length} shorter than 1 byte.");
                    }

                    return Data;
                }
            }

            throw new CompilationException("(Word) Unable to get bytes of word with null constant-value and null byte-value");
        }

        public override string ToString ()
        {
            if (!string.IsNullOrWhiteSpace(Identifier)) return Identifier;
            else if (Data != null) return string.Join("", Data!);
            else return "Word(Empty)";
        }

        public bool IsTargetingRegister ()
        {
            return TargetingRegister;
        }

        public int GetWidth (RFASMCompiler compiler)
        {
            return compiler.Options!.DataWidth;
        }

        public static byte[] MakeByteArr (List<Word> words)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < words.Count; i++)
                bytes.AddRange(words[i].Data);
            return bytes.ToArray();
        }
    }
}
