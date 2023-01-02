using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core
{
    public class BasicProgram
    {
        public IList<BasicStatement> Statements { get; }

        public BasicProgram()
        {
            Statements = new List<BasicStatement>();
        }
    }
}
