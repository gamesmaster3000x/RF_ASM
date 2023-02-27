using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class RegSetBStatement : BasicStatement
    {
        public string Register { get; set; }
        public string Value { get; set; }

        public RegSetBStatement(string register, string value)
        {
            Register = register;
            Value = value;
        }

        public override string ToString()
        {
            return $"reg_set {Register} {Value}";
        }
    }
}
