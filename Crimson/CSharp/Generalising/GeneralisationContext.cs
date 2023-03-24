namespace Crimson.CSharp.Generalising
{
    public class GeneralisationContext
    {
        private HashSet<string> Subroutines { get; set; }

        public GeneralisationContext ()
        {
            Subroutines = new HashSet<string>();
            StackPointerStack = new Stack<int>();
        }

        /// <summary>
        /// Adds the given name to a set of names of the subroutines contained within this context.
        /// Fails if the given name already exists in the set.
        /// There should be no duplicates at this stage because it would cause errors during linking,
        /// but it's better to be safe than sorry because I really don't trust this process to not goof
        /// sometimes.
        /// </summary>
        /// <param name="sub">The name to check.</param>
        /// <returns>The input name.</returns>
        /// <exception cref="DuplicateNameException">If the given name has been reserved before.</exception>
        public string ReserveSubroutineName (string sub)
        {
            if (!Subroutines.Add(sub)) throw new DuplicateNameException($"Cannot add second subroutine with name {sub} to GeneralisationContext.");
            return sub;
        }


        // ==== STACK MEMORY ====


        /// <summary>
        /// Starts a new stack frame to be used by the scope allocation head. 
        /// Pushes the old offset onto the C# stack pointer stack to save it for later.
        /// </summary>
        public void EnterScope ()
        {
            StackPointerStack.Push(_virtualStackPointer);
            _virtualStackPointer = 0;
        }

        /// <summary>
        /// Called when leaving a scope.
        /// Resets the virtual stack pointer's position to its value before the exiting scope was entered.
        /// </summary>
        public void LeaveScope ()
        {
            _virtualStackPointer = StackPointerStack.Pop();
        }

        /// <summary>
        /// A stack to keep track of the virtual stack pointer's location in different nested scopes.
        /// </summary>
        private Stack<int> StackPointerStack { get; set; }

        /// <summary>
        /// The offset from the stack frame pointer at which memory should be allocated.
        /// Tracked through different scopes with the stack pointer stack.
        /// </summary>
        private int _virtualStackPointer = 0;

        /// <summary>
        /// Reserves the given number of bytes on the virtual stack.
        /// Used to determine the offset from the stack frame pointer at which scope (stack) 
        /// variables will be stored and referenced from.
        /// </summary>
        /// <param name="size">The amount of memory to reserve, in bytes.</param>
        /// <returns>The starting address at which the block was allocated.</returns>
        public int AllocStack (int size)
        {
            int addr = _globalAllocationHead;
            _globalAllocationHead += size;
            return addr;
        }


        // ==== GLOBAL MEMORY ====


        /// <summary>
        /// Tracks the offset from the start of program memory to where the next global variable can be allocated.
        /// </summary>
        private int _globalAllocationHead = 0;

        /// <summary>
        /// Reserves the given number of bytes and return the start offset for the chunk.
        /// Used to an address to a global variable, relative to the start of program memory.
        /// </summary>
        /// <param name="size">The amount of memory to reserve, in bytes.</param>
        /// <returns>The starting address at which the block was allocated.</returns>
        public int AllocGlobal (int size)
        {
            int addr = _globalAllocationHead;
            _globalAllocationHead += size;
            return addr;
        }


        // ====  ====


        public class DuplicateNameException : Exception
        {
            public DuplicateNameException (string msg) : base(msg)
            {
            }
        }

    }
}