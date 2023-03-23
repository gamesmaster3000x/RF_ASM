namespace Crimson.CSharp.Assembly.RFASM
{
    internal class RFASMProgram : AbstractAssemblyProgram
    {

        public List<Fragment<RFASMStatement>> Fragments { get; }

        public RFASMProgram ()
        {
            Fragments = new List<Fragment<RFASMStatement>>();
        }

        public void Add (Fragment<RFASMStatement> f)
        {
            Fragments.Add(f);
        }

        public void Add (params RFASMStatement[] statements)
        {
            Fragment<RFASMStatement> f = new Fragment<RFASMStatement>(0);
            f.Add(statements);
            Add(f);
        }

        public void Add (RFASMStatement statement)
        {
            Fragment<RFASMStatement> f = new Fragment<RFASMStatement>(0);
            f.Add(statement);
            Fragments.Add(f);
        }
    }
}