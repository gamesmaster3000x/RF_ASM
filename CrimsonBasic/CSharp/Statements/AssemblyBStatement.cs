namespace CrimsonBasic.CSharp.Statements
{
    public class AssemblyBStatement : BasicStatement
    {
        public string Text { get; protected set; }

        public AssemblyBStatement (string text)
        {
            Text = text;
        }

        public override string ToString ()
        {
            return $"A~ \"{Text}\"";
        }
    }
}