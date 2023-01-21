using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class ValueConfiguration: IConfiguration
    {
        private string _id;
        private Word _word;

        public ValueConfiguration(string id, Word word)
        {
            _id = id;
            _word = word;
        }
    }
}
