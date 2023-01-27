using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    public class HeapMemoryAllocatorCStatement
    {
        public FunctionCStatement.Header Header { get; private set; }

        public HeapMemoryAllocatorCStatement(FunctionCStatement.Header header) : base()
        {
            Header = header;
        }
    }
}