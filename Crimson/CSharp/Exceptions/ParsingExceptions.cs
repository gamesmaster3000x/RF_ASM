using Crimson.CSharp.Core;
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

    internal class UriSchemeException : CrimsonException
    {
        public Uri URI { get; private set; }

        public UriSchemeException (Uri uri) : base(Core.Crimson.PanicCode.PARSE_URI_SCHEME)
        {
            URI = uri;
        }

        public override IList<string> GetDetailedMessage ()
        {
            List<string> strings = new List<string>
            {
                $"The URI '{URI}' has a disallowed scheme: '{URI.Scheme}'.",
                $"Only {String.Join(',', URIs.AcceptedSchemes)} are allowed."
            };
            return strings;
        }
    }

    internal class UriHostException : CrimsonException
    {
        public Uri URI { get; private set; }

        public UriHostException (Uri uri) : base(Core.Crimson.PanicCode.PARSE_URI_HOST)
        {
            URI = uri;
        }

        public override IList<string> GetDetailedMessage ()
        {
            List<string> strings = new List<string>
            {
                $"The URI '{URI}' host '{URI.Host}' could not be resolved.",
                $"Please check that the given host is permitted in the current context.",
                $"Crimson's custom hosts are: {string.Join(',', URIs.CustomHosts)}"
            };
            return strings;
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
