using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class RegisterBStatement : BasicStatement
    {
        private RegisterOperation _operation;
        private string[] _arguments;

        public RegisterBStatement(RegisterOperation operation, params string[] arguments)
        {
            _operation = operation;
            _arguments = arguments;
        }

        public override string ToString()
        {
            return $"register {_operation.ToString().ToLower()} {string.Join(' ', _arguments)}";
        }

        public enum RegisterOperation
        {
            SET,
            MOVE
        }
    }
}
