namespace CrimsonBasic.CSharp.Statements
{
    public class AssemblyBStatement : BasicStatement
    {
        private string _text;

        public AssemblyBStatement(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}