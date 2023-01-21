using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class Word
    {
        private byte[] _data;

        public Word(byte[] data)
        {
            _data = data;
        }
    }
}
