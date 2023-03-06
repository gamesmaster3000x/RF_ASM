using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class PushSfBStatement : BasicStatement
    {

        public PushSfBStatement()
        {
        }

        public override string ToString()
        {
            return $"push_sf;";
        }
    }
}
