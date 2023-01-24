using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class LabelBStatement : BasicStatement
    {
        private string _name;

        public LabelBStatement(string label)
        {
            _name = label;
        }

        public override string ToString()
        {
            return _name.StartsWith(":") ? _name : ":" + _name;
        }
    }
}
