using NLog.Targets;
using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RedFoxAssembly.CSharp.Statements.IData;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class Word : IData
    {
        private bool _isTargetingRegister;
        private bool _isTargetingLabel;
        private byte[]? _data;
        private string? _identifier;

        public Word(bool isTargetingRegister, byte[] data)
        {
            _isTargetingRegister = isTargetingRegister;

            if (data == null | data.Length < 1) throw new ParsingException("Cannot assign fewer than 1 byte to a word");
            _data = data;
        }

        public Word(bool isTargetingRegister, string identifier)
        {
            _isTargetingRegister = isTargetingRegister;

            if (String.IsNullOrWhiteSpace(identifier)) throw new ParsingException("Cannot assign a null or whitespace value-mapping to a word");
            _identifier = identifier;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            // Return the value of the named constant 
            if (_identifier != null)
            {
                if (compiler.Constants.TryGetValue(_identifier, out IData? val))
                {
                    byte[] v = val.GetBytes(compiler);
                    if (v.Length != compiler.meta!.DataWidth)
                        throw new CompilationException($"Constant {_identifier} with value [{String.Join(',', v)}] is not of correct width {compiler.meta.DataWidth} for word.");
                    return v;
                }
                else
                {
                    throw new CompilationException("(Word) Cannot get byte value of undeclared constant value " + val);
                }
            }

            // Return _data
            if (_data != null)
            {

                // Is targeting register, so return register address (1 byte only)
                if (IsTargetingRegister())
                {
                    if (_data.Length != 1)
                        throw new CompilationException($"Word [{String.Join(',', _data)}] targetting register is length {_data.Length}, but it should have length 1.");
                    else
                        return _data;
                }

                // Not targeting register, so return whole value (must be of correct data width)
                else
                {
                    // Illegal data width
                    if (_data.Length != compiler.meta!.DataWidth)
                    {
                        throw new CompilationException($"Width {_data.Length} of word [{String.Join(',', _data)}] does not match defined data width {compiler.meta.DataWidth}.");
                    }
                    // If contains no data
                    else if (_data.Length < 1)
                    {
                        throw new CompilationException($"Word [{String.Join(',', _data)}] width {_data.Length} shorter than 1 byte.");
                    }

                    return _data;
                }
            }

            throw new CompilationException("(Word) Unable to get bytes of word with null constant-value and null byte-value");
        }

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(_identifier)) return _identifier;
            else if (_data != null) return String.Join("", _data!);
            else return "Word(Empty)";
        }

        public bool IsTargetingRegister()
        {
            return _isTargetingRegister;
        }
    }
}
