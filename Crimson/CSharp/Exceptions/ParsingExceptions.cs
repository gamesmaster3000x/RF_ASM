using Crimson.CSharp.Core;
using Crimson.CSharp.Core.CURI;
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
        public string Message { get; private set; }
        public AbstractCrimsonStatement? Statement { get; private set; }
        public Exception? Cause { get; private set; }

        public StatementParseException (string message, AbstractCrimsonStatement? statement, Exception? cause) : base(Core.Crimson.PanicCode.PARSE_STATEMENT)
        {
            Message = message;
            Statement = statement;
            Cause = cause;
        }

        public override IList<string> GetDetailedMessage ()
        {
            List<string> strings = new List<string>()
            {
                $"An error occurred while parsing the statement {(Statement != null ? Statement : "NULL")}",
                Message
            };
            IEnumerable<string>? formatted = FormatException(Cause);
            strings.AddRange(formatted ?? Enumerable.Empty<string>());
            return strings;
        }
    }

    internal class CURIException : CrimsonException
    {
        public override string Message { get; }
        public AbstractCURI? CURI { get; private set; }
        public CURIExceptionReason Reason { get; private set; }

        public CURIException (string message, AbstractCURI uri, CURIExceptionReason reason) : base(Core.Crimson.PanicCode.CURI)
        {
            Message = message;
            CURI = uri;
            Reason = reason;
        }

        public override IList<string> GetDetailedMessage ()
        {
            List<string> strings = new List<string>
            {
                $"The Crimson URI '{CURI}' is invalid in the given context.",
                Message
            };

            string extra = Reason switch
            {
                CURIExceptionReason.SCHEME => $"Scheme: {CURI.Scheme}.",
                CURIExceptionReason.HOST => $"Host: {CURI.Uri.Host}.",
                CURIExceptionReason.PATH => $"Path: {CURI.Uri.AbsolutePath}.",
                _ => $"No extra information.",
            };
            strings.Add(extra);

            return strings;
        }

        public enum CURIExceptionReason
        {
            SCHEME, HOST, PATH
        }
    }

    internal class CrimsonParserException : CrimsonException
    {
        public CrimsonParserException (string message) : base(Core.Crimson.PanicCode.PARSE_STATEMENT)
        {
        }
    }

    internal class ScopeGenerationException : CrimsonException
    {
        public ScopeGenerationException () : base(Core.Crimson.PanicCode.PARSE_SCOPE)
        {

        }
    }
}
