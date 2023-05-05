using Antlr4.Runtime;
using NLog;
using RedFoxAssembly.Exceptions;

namespace RedFoxAssembly.Antlr
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Path { get; set; }
        public LexerErrorListener (string path)
        {
            Path = path;
        }

        void IAntlrErrorListener<int>.SyntaxError (TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            string message = $"Lexer error lexing {Path} @ line:{line} char:{charPositionInLine} msg:{msg}";
            LOGGER.Error(message);
            throw new ParsingException("LEXER ERROR: " + message, e);
        }
    }
}
