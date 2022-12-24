using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {

        private string Path { get; set; }
        public LexerErrorListener(string path)
        {
            Path = path;
        }

        void IAntlrErrorListener<int>.SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new NotImplementedException();
        }
    }
}
