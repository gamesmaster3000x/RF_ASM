using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class StackBStatement : BasicStatement
    {
        private StackOperation _operation;
        private string[] _arguments;

        public StackBStatement(StackOperation operation, params string[] arguments)
        {
            _operation = operation;
            _arguments = arguments;
        }

        public override string ToString()
        {
            return $"stack {_operation.ToString().ToLower()} {string.Join(' ', _arguments)};";
        }

        public enum StackOperation
        {
            PUSH_FRAME,
            POP_FRAME,
            ALLOCATE,
            DEALLOCATE
        }
    }
}
