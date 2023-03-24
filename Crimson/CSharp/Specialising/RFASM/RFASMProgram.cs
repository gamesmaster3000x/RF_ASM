using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Specialising.RFASM
{
    internal class RFASMProgram : AbstractSpecificAssemblyProgram
    {

        public List<Fragment> Fragments { get; }

        public RFASMProgram()
        {
            Fragments = new List<Fragment>();
        }

        public void Add(Fragment f)
        {
            Fragments.Add(f);
        }

        public void Add(params RFASMStatement[] statements)
        {
            Fragment f = new Fragment(0);
            f.Add(statements);
            Add(f);
        }

        public void Add(RFASMStatement statement)
        {
            Fragment f = new Fragment(0);
            f.Add(statement);
            Fragments.Add(f);
        }

        internal override IEnumerable<Fragment> GetFragments()
        {
            return Fragments;
        }
    }
}