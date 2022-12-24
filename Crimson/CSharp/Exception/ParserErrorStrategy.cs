using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class ParserErrorStrategy : IAntlrErrorStrategy
    {

        private string Path { get; set; }
        public ParserErrorStrategy(string path)
        {
            Path = path;
        }

        bool IAntlrErrorStrategy.InErrorRecoveryMode(Parser parser)
        {
            throw new NotImplementedException();
        }

        void IAntlrErrorStrategy.Recover(Parser parser, RecognitionException e)
        {
            throw new NotImplementedException();
        }

        IToken IAntlrErrorStrategy.RecoverInline(Parser parser)
        {
            throw new NotImplementedException();
        }

        void IAntlrErrorStrategy.ReportError(Parser parser, RecognitionException e)
        {
            throw new NotImplementedException();
        }

        void IAntlrErrorStrategy.ReportMatch(Parser parser)
        {
            throw new NotImplementedException();
        }

        void IAntlrErrorStrategy.Reset(Parser parser)
        {
            throw new NotImplementedException();
        }

        void IAntlrErrorStrategy.Sync(Parser parser)
        {
            throw new NotImplementedException();
        }
    }
}
