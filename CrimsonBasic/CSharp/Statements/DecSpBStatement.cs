using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class DecSpBStatement : BasicStatement
    {
        public int Amount { get; set; }

        public DecSpBStatement (int amount)
        {
            Amount = amount;
        }

        public override string ToString()
        {
            return $"dec_sp {Amount};";
        }
    }
}
