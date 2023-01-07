using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class JumpNotEqualBStatement : BasicStatement
    {
        private string _name;

        public JumpNotEqualBStatement(string label)
        {
            _name = label;
        }

        public override string ToString()
        {
            return $"jump {_name}";
        }
    }
}
