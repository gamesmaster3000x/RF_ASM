using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class TestBStatement : BasicStatement
    {
        public TestBStatement(string text): base(text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
