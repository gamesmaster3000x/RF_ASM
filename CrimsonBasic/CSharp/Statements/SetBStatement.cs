using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class SetBStatement : BasicStatement
    {

        private string _value;
        private string _name;

        public SetBStatement(string text, string value)
        {
            this._name = text;
            this._value = value;
        }

        public override string ToString()
        {
            return $"set {_name} {_value};";
        }
    }
}
