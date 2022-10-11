using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class InstructionToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        private Instructions value;

        private AddressingMode mode1;
        private AddressingMode mode2;

        public InstructionToken(Instructions value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            this.value = value;
            TokenType = TokenType.INSTRUCTION;
            RawValue = Enum.GetName(value.GetType(), value);
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[] { (byte)value };
            byte[] fitBytes = CompilerUtils.FitToDataWidth(meta.DataWidth, bytes);

            byte addressingModeByte = GetAddressingModesByte();
            byte firstByte = (byte)(fitBytes[0] | addressingModeByte);
            fitBytes[0] = firstByte;

            return fitBytes;
        }

        private byte GetAddressingModesByte()
        {
            if (mode1 == null || mode2 == null) {
               throw new NullReferenceException("Cannot understand addressing modes " + nameof(mode1) + " and " + nameof(mode2));
            }

            byte modeOne = (byte)(mode1 == AddressingMode.RAW ? 0 : 1);
            byte modeTwo = (byte)(mode2 == AddressingMode.RAW ? 0 : 1);

            byte shiftedOne = (byte)(modeOne << 1);
            byte combined = (byte)(shiftedOne | modeTwo);

            byte shiftedAll = (byte)(combined << 6);

            return shiftedAll;
        }

        // TODO InstructionToken syntax
        public override bool CheckFollowing(IToken[] following)
        {
            if (following[0] != null && following[0] is AbstractValueToken)
            {
                AbstractValueToken followingValue1 = (AbstractValueToken) following[0];
                mode1 = followingValue1.ValueType;
            }
            if (following[1] != null && following[1] is AbstractValueToken)
            {
                AbstractValueToken followingValue2 = (AbstractValueToken) following[1];
                mode2 = followingValue2.ValueType;
            }
            return true;
        }
    }
}
