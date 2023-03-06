using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Statements
{
    public class SetBStatement : BasicStatement
    {

        public string Value { get; protected set; }
        public int Size { get; protected set; }
        public string Name { get; protected set; }

        public SetBStatement(string name, int size, string value)
        {
            Name = name;
            Size = size;
            Value = value;
        }

        public override string ToString()
        {
            return $"var_set {Name}, {Size}, {Value};";
        }
    }
}
