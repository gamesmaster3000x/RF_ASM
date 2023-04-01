using Antlr4.Runtime;
using NLog;

namespace Crimson.CSharp.Exceptions
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Name { get; set; }
        public LexerErrorListener (string name)
        {
            Name = name;
        }

        void IAntlrErrorListener<int>.SyntaxError (TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            string message = $"Lexer error lexing {Name} @ line:{line} char:{charPositionInLine} msg:{msg}";
            LOGGER.Error(message);
            throw new LexerException(message, e);
        }
    }
}
