using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class JumpSubBStatement : BasicStatement
    {
        private string _label;

        public JumpSubBStatement (string _label)
        {
            this._label = _label;
        }

        public override string ToString ()
        {
            return $"jump {_label}";
        }
    }
}
