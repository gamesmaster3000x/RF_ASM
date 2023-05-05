namespace Compiler.Common.Exceptions
{
    internal class GeneralisingException : ArgumentException
    {
        public GeneralisingException(string message) : base(message)
        {

        }

        public GeneralisingException(string message, Exception cause) : base(message, cause)
        {

        }
    }
}
