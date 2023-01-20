using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class HeapBStatement : BasicStatement
    {
        private HeapOperation _operation;
        private string[] _arguments;

        public HeapBStatement(HeapOperation operation, params string[] arguments)
        {
            _operation = operation;
            _arguments = arguments;
        }

        public override string ToString()
        {
            return $"heap {_operation.ToString().ToLower()} {String.Join(' ', _arguments)}";
        }

        public enum HeapOperation
        {
            ALLOCATE,
            DEALLOCATE
        }
    }
}
