using Crimson.CSharp.Exceptions;
using NLog;

namespace Crimson.CSharp.Core
{
    internal class ExceptionCrimsonException : CrimsonException
    {
        public string ErrMessage { get; }
        public Exception E { get; }

        public ExceptionCrimsonException (string message, Crimson.PanicCode code, Exception e) : base(code)
        {
            ErrMessage = message;
            E = e;
        }

        public override IList<string> GetDetailedMessage ()
        {
            List<string> message = new List<string>
            {
                $"This is an automatically generated error-message. An unexpected non-{typeof(CrimsonException)} has occurred. Details may be limited.",
                $"Please report this error to the maintainer of this software, including logs, system details, .NET version and any other relevant information."
            };

            //
            if (String.IsNullOrWhiteSpace(ErrMessage))
                message.Add("No unique error message given.");
            else
                message.Add(ErrMessage);

            //
            if (E == null)
                message.Add("No exception details provided.");
            else
                message.AddRange(E.ToString().Split(Environment.NewLine));

            //
            return message;
        }
    }
}