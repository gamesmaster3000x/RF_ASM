using CrimsonBasic.CSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class JumpEqualBStatement : BasicStatement
    {
        private string _value1;
        private string _value2;
        private string _label;

        public JumpEqualBStatement(string _value1, string _value2, string _label)
        {
            if (string.IsNullOrWhiteSpace(_value1)) throw new ArgumentException($"Value 1 '{_value1}' in JumpEqualBStatement statement may not be null or whitespace.");
            if (string.IsNullOrWhiteSpace(_value2)) throw new ArgumentException($"Value 2 '{_value2}' in JumpEqualBStatement statement may not be null or whitespace.");
            if (string.IsNullOrWhiteSpace(_label)) throw new ArgumentException($"Target label '{_label}' in JumpEqualBStatement statement may not be null or whitespace.");
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
