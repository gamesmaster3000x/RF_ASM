
using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class LabelDeclarationToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        public LabelDeclarationToken(string value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            Type = TokenType.LABEL;
            RawValue = value.Replace(":", "", StringComparison.Ordinal);
        }

        public override byte[] GetBytes()
        {
            return new byte[0];
        }

        public override bool HasCorrectSyntax(IToken[] following)
        {
            return true;
        }

        internal List<IToken> DeclareLabel(RFASMCompiler compiler, int index)
        {
            byte[] val = BitConverter.GetBytes(index);

            // Take the string value (5C) and convert it to 005C (or whatever the data width is)
            byte[] bytes = BitConverter.GetBytes(index * meta.DataWidth).Reverse().ToArray();

            meta.constants.Add(RawValue, Utils.FitToDataWidth(meta.DataWidth, bytes)); // TODO DeclareLabel index might truncate things!!

            return new List<IToken>();
        }
    }
}
