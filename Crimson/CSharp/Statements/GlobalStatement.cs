namespace Crimson.CSharp.Statements
{
    public abstract class GlobalStatement : CrimsonStatement
    {
        public string Name { get; set; }
        protected GlobalStatement() : base(false)
        {
            Name = "";
        }
    }
}