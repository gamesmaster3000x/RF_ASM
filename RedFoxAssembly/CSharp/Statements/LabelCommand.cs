using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class LabelCommand: ICommand
    {
        private string _id;

        public LabelCommand(string id)
        {
            _id = id;
        }
    }
}
