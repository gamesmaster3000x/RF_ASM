using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class ValueConfiguration: IConfiguration
    {
        private string _id;
        private Word? _word;
        private RByte? _byte;

        public ValueConfiguration(string id, Word word)
        {
            _id = id;
            _word = word;

            if (String.IsNullOrWhiteSpace(_id)) throw new ParsingException($"Cannot create ValueConfiguration of word value '{_word}' with null or whitespace id '{id}'");
            if (_word == null) throw new ParsingException($"Cannot create ValueConfiguration '{id}' with null word value '{_word}'");
        }

        public ValueConfiguration(string id, RByte @byte)
        {
            _id = id;
            _byte = @byte; 
            
            if (String.IsNullOrWhiteSpace(_id)) throw new ParsingException($"Cannot create ValueConfiguration of byte value '{_byte}' with null or whitespace id '{id}'");
            if (_byte == null) throw new ParsingException($"Cannot create ValueConfiguration '{id}' with null byte value '{_byte}'");
        }

        void IConfiguration.Resolve(RFASMCompiler compiler)
        {
            if (compiler.Constants.ContainsKey(_id)) throw new PreCompilationException($"Illegal duplicate declaration of constant value {_id}");
            
            if (_word != null) compiler.Constants.Add(_id, _word);
            else if (_byte != null) compiler.Constants.Add(_id, _byte);
            else throw new PreCompilationException($"Cannot declare value {_id} with no word or byte value");
        }
    }
}
