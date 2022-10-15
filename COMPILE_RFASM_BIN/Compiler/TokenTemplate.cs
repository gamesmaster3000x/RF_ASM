using RedFoxAssembly.Compiler.Tokens;

namespace RedFoxAssembly.Compiler
{
    public class TokenTemplate
    {
        private AddressingMode tokenValueType;
        private string value;
        private RFASMCompilerMetadata meta;

        public TokenTemplate(AddressingMode tokenValueType, string value, RFASMCompilerMetadata meta)
        {
            this.tokenValueType = tokenValueType;
            this.value = value ?? throw new ArgumentNullException(nameof(value));
            this.meta = meta ?? throw new ArgumentNullException(nameof(meta));
        }

        internal AbstractValueToken CreateToken()
        {
            switch (tokenValueType)
            {
                case AddressingMode.RAW:
                    return new RawValueToken(value, meta);
                case AddressingMode.REGISTER:
                    return new RegisterAddressToken(value, meta);
            }

            throw new ArgumentException("Cannot resolve a new AbstractValueToken of TokenValueType " + tokenValueType);
        }
    }
}