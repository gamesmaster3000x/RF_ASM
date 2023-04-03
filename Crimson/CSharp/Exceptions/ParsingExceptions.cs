using Crimson.CSharp.Parsing.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exceptions
{
    internal class StatementParseException : CrimsonException
    {
        public AbstractCrimsonStatement Statement { get; private set; }
        public Exception Cause { get; private set; }

        public StatementParseException (AbstractCrimsonStatement statement, Exception cause) : base(Core.Crimson.PanicCode.PARSE_STATEMENT)
        {
            Statement = statement;
            Cause = cause;
        }

        public override IList<string> GetDetailedMessage ()
        {
            List<string> strings = new List<string>();
            strings.Add($"An error occurred while parsing the statement {(Statement != null ? Statement : "NULL")}");
            strings.AddRange(FormatException(Cause));
            return strings;
        }
    }

    internal class CrimsonParserException : ArgumentException
    {
        public CrimsonParserException (string message) : base(message)
        {

        }

        public CrimsonParserException (string message, Exception cause) : base(message, cause)
        {

        }
    }

    internal class ScopeGenerationException : CrimsonException
    {
        public ScopeGenerationException (string message) : base(message)
        {

        }

        public ScopeGenerationException (string message, Exception cause) : base(message, cause)
        {

        }
    }
}
