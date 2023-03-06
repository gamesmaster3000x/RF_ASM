using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class IncSpBStatement : BasicStatement
    {
        public int Amount { get; set; }

        public IncSpBStatement (int amount)
        {
            Amount = amount;
        }

        public override string ToString()
        {
            return $"inc_sp {Amount};";
        }
    }
}
