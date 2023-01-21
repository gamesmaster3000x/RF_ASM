using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class WidthConfiguration: IConfiguration
    {
        private int _width;

        public WidthConfiguration(int width)
        {
            _width = width;
        }
    }
}
