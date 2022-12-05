using RedFoxAssembly.Compiler;
using RedFoxAssembly.TokenParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Compiler.Tokens
{
    internal abstract class AbstractValueToken : AbstractToken
    {
        public RFASMCompilerMetadata meta;
        private AddressingMode _valueType;
        public AddressingMode ValueType { get => _valueType; set => _valueType = value; }
        public AbstractValueToken(string value, RFASMCompilerMetadata meta, AddressingMode type)
        {
            this.meta = meta ?? throw new ArgumentNullException("Metadata cannot be null when creating an AbstractValueToken");
            this.TokenType = TokenType.VALUE;
            this.ValueType = type;
            this.RawValue = ParseRawValue(value) ?? throw new ArgumentNullException("RawValue cannot be null when creating an AbstractValueToken");
        }
        public abstract string ParseRawValue(string reallyRawValue);

        public override byte[] GetBytes()
        {
            byte[] ba = Convert.FromHexString(RawValue);
            if (ba.Length != meta.DataWidth)
            {
                throw new CompilationException("Data width " + ba.Length + " of ValueToken " + RawValue + " does not match expected width " + meta.DataWidth);
            }
            return CompilerUtils.FitToDataWidth(meta.DataWidth, ba);
        }
    }
}
