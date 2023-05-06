namespace Compiler.Common.Exceptions
{
    public abstract class CrimsonCoreException : Exception
    {
        public CrimsonCoreException(Program.PanicCode code)
        {
            Code = code;
        }

        public Program.PanicCode Code { get; private set; }

        public virtual IList<string> GetDetailedMessage()
        {
            return new List<string>() { "No additional message detail provided." };
        }

        public virtual IList<string>? FormatException(Exception? e)
        {
            return e?.ToString()?.Split(Environment.NewLine);
        }
    }
}
