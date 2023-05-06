namespace RedFoxAssembly.Exceptions
{
    internal class ParsingException : Exception
    {
        public ParsingException (string error) : base(error) { }
        public ParsingException (string error, Exception cause) : base(error, cause) { }
    }
}
