using Compiler.Parser.Syntax.Functions;
using Compiler.Parser.Syntax.Variables;

namespace Compiler.Generaliser
{
    public class GeneralisationContext
    {
        public Dictionary<string, GlobalVariable> Globals { get; }
        public Dictionary<string, Function> Functions { get; }
        public Dictionary<string, Mask> Structures { get; }

        public GeneralisationContext (Dictionary<string, Function> functions, Dictionary<string, Mask> structures, Dictionary<string, GlobalVariable> globals)
        {
            StackPointerStack = new Stack<int>();
            Functions = functions;
            Structures = structures;
            Globals = globals;
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