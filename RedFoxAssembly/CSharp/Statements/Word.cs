using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class Word : IData
    {
        private byte[]? _data;
        private string? _value;

        public Word(byte[] data)
        {
            if (data == null | data.Length < 1) throw new ParsingException("Cannot assign fewer than 1 byte to a word");
            _data = data;
        }

        public Word(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) throw new ParsingException("Cannot assign a null or whitespace value-mapping to a word");
            _value = value;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            if (_value != null)
                if (compiler.Constants.TryGetValue(_value, out IData? val))
                    return val.GetBytes(compiler);
                else
                    throw new CompilationException("Cannot get byte value of undeclared constant value " + val);

            if (_data != null)
                if (_data.Length > 0)
                    return _data;
                else throw new CompilationException("Word cannot return a null byte array");

            throw new CompilationException("Unable to get bytes of word with null constant-value and null byte-value");
        }
    }
}
