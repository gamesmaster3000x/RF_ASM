using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class JumpBStatement : BasicStatement
    {
        private string _label;

        public JumpBStatement(string _label)
        {
            this._label = _label;
        }

        public override string ToString()
        {
            return $"jump {_label}";
        }
    }
}
