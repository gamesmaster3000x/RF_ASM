namespace Crimson.CSharp.Generalising
{
    public class GeneralisationContext
    {
        private HashSet<string> Subroutines { get; set; } = new HashSet<string>();

        public string ReserveSubroutineName (string sub)
        {
            if (!Subroutines.Add(sub)) throw new DuplicateNameException($"Cannot add second subroutine with name {sub} to GeneralisationContext.");
            return sub;
        }

        private int _globalAllocationHead = 0;
        public int AllocGlobal (int size)
        {
            int addr = _globalAllocationHead;
            _globalAllocationHead += size;
            return addr;
        }

        public class DuplicateNameException : Exception
        {
            public DuplicateNameException (string msg) : base(msg)
            {
            }
        }

    }
}