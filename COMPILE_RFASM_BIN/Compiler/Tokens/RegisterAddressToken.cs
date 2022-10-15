using RedFoxAssembly.Compiler;
using RedFoxAssembly.TokenParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Compiler.Tokens
{
    internal class RegisterAddressToken: AbstractValueToken
    {
        public RegisterAddressToken(string value, RFASMCompilerMetadata meta): base(value, meta, AddressingMode.REGISTER)
        {
        }

        public override byte[] GetBytes()
        {
            byte[] ba = Convert.FromHexString(RawValue);
            if (ba.Length != meta.DataWidth)
            {
                throw new CompilationException("Data width " + ba.Length + " of RegisterAddressToken " + RawValue + " does not match expected width " + meta.DataWidth);
            }
            return CompilerUtils.FitToDataWidth(meta.DataWidth, ba);
        }

        public override string ParseRawValue(string reallyRawValue)
        {
            return reallyRawValue.Replace("*", "", StringComparison.Ordinal);
        }

        public override bool CheckFollowing(IToken[] following)
        {
            return true;
        }
    }
}
