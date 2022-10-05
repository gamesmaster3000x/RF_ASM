using RFASM_COMPILER.TOKEN_PARSER;

namespace RFASM_COMPILER.RFASM_BIN
{
    internal class RFASMCompilerMetadata : ITokenParserMetadata
    {
        public string INPUT_PATH;
        public int DATA_WIDTH;

        int ITokenParserMetadata.GetDataWidth()
        {
            return DATA_WIDTH;
        }
    }
}