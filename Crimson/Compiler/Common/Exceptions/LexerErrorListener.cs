using Antlr4.Runtime;
using NLog;

namespace Compiler.Common.Exceptions
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Name { get; set; }
        public LexerErrorListener(string name)
        {
            Name = name;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Panicker.Panic($"A lexer error has occurred lexing {Name}", PanicCode.COMPILE_PARSE, null!);
        }
    }
}
