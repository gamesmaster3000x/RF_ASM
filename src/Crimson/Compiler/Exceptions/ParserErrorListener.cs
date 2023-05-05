using Antlr4.Runtime;
using NLog;

namespace Compiler.Exceptions
{
    internal class ParserErrorListener : IAntlrErrorListener<IToken>
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Name { get; set; }
        public ParserErrorListener (string name)
        {
            Name = name;
        }

        public void SyntaxError (TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            CrimsonCore.Panic($"A parser error has occurred parsing {Name}", CrimsonCore.PanicCode.COMPILE_PARSE, null!);
        }
    }
}
