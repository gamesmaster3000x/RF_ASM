using Compiler.Common.CURI;
using Compiler.Parser.Syntax.Values;
using Compiler.Parsing.Syntax;

namespace Compiler.Common.Exceptions
{

    internal class StatementParseException : CrimsonCoreException
    {
        public string Message { get; private set; }
        public object Statement { get; private set; }
        public Exception? Cause { get; private set; }

        public StatementParseException (string message, object statement, Exception? cause) : base(PanicCode.COMPILE_PARSE_STATEMENT)
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

    internal class MultiMaskParametersException : CrimsonCoreException
    {
        private string MaskName { get; }
        private Dictionary<string, ISimpleValue> MaskParameters { get; }

        public MultiMaskParametersException (string maskName, Dictionary<string, ISimpleValue> maskParameters) : base(PanicCode.COMPILE_PARSE_STATEMENT_MASK) //TODO mask param excep
        {
            MaskName = maskName;
            MaskParameters = maskParameters;
        }

        public override IList<string> GetDetailedMessage ()
        {
            IList<string> strings = new List<string>()
            {
                $"Unable to parse the parameters for the multi-mask '{MaskName}'.",
                $"A multi-mask must have at least 1 parameter, but only {MaskParameters.Count}.",
                "",
                $"Parameters: "
            };
            foreach (var p in MaskParameters)
            {
                strings.Add($"{p.Key}<{p.Value}>");
            }
            strings.Add("");
            return strings;
        }
    }

    internal class CURIException : CrimsonCoreException
    {
        public override string Message { get; }
        public AbstractCURI CURI { get; private set; }
        public CURIExceptionReason Reason { get; private set; }

        public CURIException (string message, AbstractCURI uri, CURIExceptionReason reason) : base(PanicCode.CURI)
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
                CURIExceptionReason.SCHEME => $"Scheme: {CURI.Uri.Scheme}.",
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

    internal class CrimsonParserException : CrimsonCoreException
    {
        public CrimsonParserException (string message) : base(PanicCode.COMPILE_PARSE_STATEMENT)
        {
        }
    }

    internal class ScopeGenerationException : CrimsonCoreException
    {
        public ScopeGenerationException () : base(PanicCode.COMPILE_PARSE_SCOPE)
        {

        }
    }
}
