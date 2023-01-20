using CrimsonBasic.CSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class JumpEqualBStatement : BasicStatement
    {
        private string _value1;
        private string _value2;
        private string _label;

        public JumpEqualBStatement(string _value1, string _value2, string _label)
        {
            if (String.IsNullOrWhiteSpace(_value1)) throw new ArgumentException("Cannot use null or whitespace name or value in Jump statement");
            if (String.IsNullOrWhiteSpace(_value2)) throw new ArgumentException("Cannot use null or whitespace name or value in Jump statement");
            if (String.IsNullOrWhiteSpace(_label)) throw new ArgumentException("Cannot use null or whitespace name in Jump statement");
            this._label = _label;
            this._value1 = _value1;
            this._value2 = _value2;
        }

        public override string ToString()
        {
            return $"jeq {_value1} {_value2} {_label}";
        }
    }
}
