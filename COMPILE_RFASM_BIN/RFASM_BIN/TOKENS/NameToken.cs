using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class NameToken: AbstractValueToken
    {
        public NameToken(string value, RFASMCompilerMetadata meta) : base(value, meta, AddressingMode.RAW)
        {
            meta.constants.TryGetValue(RawValue, out TokenTemplate template); // TODO Resolve type of NameToken at construction
        }

        public override byte[] GetBytes()
        {
            meta.constants.TryGetValue(RawValue, out TokenTemplate template);
            AbstractValueToken token = template.CreateToken();
            return CompilerUtils.FitToDataWidth(meta.DataWidth, token.GetBytes());
        }

        public override string ParseRawValue(string reallyRawValue)
        {
            return reallyRawValue.Replace("_", "", StringComparison.Ordinal);
        }

        public override bool CheckFollowing(IToken[] following)
        {
            return true;
        }

        
    }
}
