namespace Compiler.Common.Exceptions
{
    internal class LinkingException : ArgumentException
    {
        public LinkingException(string message) : base(message) { }

        public LinkingException(string message, Exception cause) : base(message, cause) { }
    }

    internal class OtherLinkingException : ArgumentException
    {
        public OtherLinkingException(string message) : base(message) { }

        public OtherLinkingException(string message, Exception cause) : base(message, cause) { }
    }
}
