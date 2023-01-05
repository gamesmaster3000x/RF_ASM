using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class LabelBStatement : BasicStatement
    {
        public LabelBStatement(string label): base(label)
        {
            Text = label;
        }

        public override string ToString()
        {
            return Text.StartsWith(":") ? Text : ":" + Text;
        }
    }
}
