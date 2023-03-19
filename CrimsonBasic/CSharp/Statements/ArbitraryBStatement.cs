namespace CrimsonBasic.CSharp.Statements
{
    public class ArbitraryBStatement : BasicStatement
    {
        public string Text { get; protected set; }

        public ArbitraryBStatement (string text)
        {
            Text = text;
        }

        public override string ToString ()
        {
            return Text;
        }
    }
}