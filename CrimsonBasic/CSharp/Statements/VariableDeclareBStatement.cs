using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class VariableDeclareBStatement : BasicStatement
    {
        private string _name;

        public VariableDeclareBStatement(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return $"var {_name};";
        }
    }
}
