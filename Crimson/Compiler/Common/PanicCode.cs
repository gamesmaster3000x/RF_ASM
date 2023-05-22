namespace Compiler.Common
{
    public enum PanicCode
    {
        OK_OR_NONE = 0,
        ARGUMENTS = -10,
        SETTINGS = -20,

        // CURI
        CURI = -100,
        CURI_STREAM = -110,

        // Compile
        COMPILE = -1000,
        COMPILE_PARSE = -1100,
        COMPILE_PARSE_STATEMENT = -1110,
        COMPILE_PARSE_STATEMENT_MASK = -1111,
        COMPILE_PARSE_SCOPE = -1120,
        COMPILE_PARSE_SCOPE_ASYNC = -1121,
        COMPILE_PARSE_SCOPE_DEPS = -1122,
        COMPILE_LINK = -1200,
        COMPILE_GENERALISE = -1300,
        COMPILE_SPECIALISE = -1400,

        // Cache
        CACHE = -2000,
        CACHE_JSON = -2010,
        CACHE_FETCH = -2020,
        CACHE_ADD = -2021,
        CACHE_INSTALL = -2100,
        CACHE_CLEAR = -2200,
    }
}