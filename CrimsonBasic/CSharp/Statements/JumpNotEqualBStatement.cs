using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class JumpNotEqualBStatement : BasicStatement
    {
        private string _value1;
        private string _value2;
        private string _label;

        public JumpNotEqualBStatement(string _value1, string _value2, string _label)
        {
            this._label = _label;
            this._value1 = _value1;
            this._value2 = _value2;
        }

        public override string ToString()
        {
            return $"jne {_value1} {_value2} {_label}";
        }
    }
}
