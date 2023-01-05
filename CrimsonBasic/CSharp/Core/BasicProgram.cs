using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimsonBasic.CSharp.Core.Statements;

namespace CrimsonBasic.CSharp.Core
{
    public class BasicProgram
    {
        public List<BasicStatement> Statements { get; }

        public BasicProgram()
        {
            Statements = new List<BasicStatement>();
        }
    }
}
