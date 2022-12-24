using Antlr4.Runtime;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class ParserErrorStrategy : IAntlrErrorStrategy
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Path { get; set; }
        public ParserErrorStrategy(string path)
        {
            Path = path;
        }

        bool IAntlrErrorStrategy.InErrorRecoveryMode(Parser parser)
        {
            LOGGER.Error("IAntlrErrorStrategy.InErrorRecoveryMode=false");
            return false;
        }

        void IAntlrErrorStrategy.Recover(Parser parser, RecognitionException e)
        {
            LOGGER.Error("IAntlrErrorStrategy.Recover");
        }

        IToken IAntlrErrorStrategy.RecoverInline(Parser parser)
        {
            LOGGER.Error("IAntlrErrorStrategy.RecoverInline");
            return null;
        }

        void IAntlrErrorStrategy.ReportError(Parser parser, RecognitionException e)
        {
            LOGGER.Error("IAntlrErrorStrategy.ReportError");
        }

        void IAntlrErrorStrategy.ReportMatch(Parser parser)
        {
            LOGGER.Error("IAntlrErrorStrategy.ReportMatch");
        }

        void IAntlrErrorStrategy.Reset(Parser parser)
        {
            LOGGER.Error("IAntlrErrorStrategy.Reset");
        }

        void IAntlrErrorStrategy.Sync(Parser parser)
        {
            LOGGER.Error("IAntlrErrorStrategy.Sync");
        }
    }
}
