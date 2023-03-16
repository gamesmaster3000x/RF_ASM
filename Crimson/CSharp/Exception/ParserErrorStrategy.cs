using Antlr4.Runtime;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exception
{
    internal class ParserErrorStrategy : DefaultErrorStrategy
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string Path { get; set; }
        public ParserErrorStrategy (string path)
        {
            Path = path;
        }

        public override void ReportError (Parser recognizer, RecognitionException e)
        {
            base.ReportError(recognizer, e);
            throw new CrimsonParserException("", e);
        }
    }
}
