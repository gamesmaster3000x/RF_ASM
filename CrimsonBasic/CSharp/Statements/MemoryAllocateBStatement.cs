using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class MemoryAllocateBStatement : BasicStatement
    {
        private int _size;
        private string _name;

        public MemoryAllocateBStatement(string text, int bytes)
        {
            _name = text;
            _size = bytes;
        }

        public int GetSize()
        {
            return _size;
        }

        public override string ToString()
        {
            return $"allocate {_name} {_size};";
        }
    }
}
