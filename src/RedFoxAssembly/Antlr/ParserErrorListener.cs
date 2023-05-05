using Antlr4.Runtime;
using NLog;
using RedFoxAssembly.Exceptions;

namespace RedFoxAssembly.Antlr
{
    internal class ParserErrorListener : BaseErrorListener
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Path { get; set; }
        public ParserErrorListener (string path)
        {
            Path = path;
        }

        public override void SyntaxError (TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            string formattedMessage = msg[0].ToString().ToUpper() + msg.Substring(1); // Capitalise first letter

            base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, formattedMessage, e);
            throw new ParsingException($"RFASM syntax error during parsing. Illegal symbol '{offendingSymbol.Text}' at {line}:{charPositionInLine}. {formattedMessage}", e);
        }
    }
}
