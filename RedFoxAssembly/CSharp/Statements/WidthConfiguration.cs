using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class WidthConfiguration : IConfiguration
    {
        private int _width;

        public WidthConfiguration(int width)
        {
            _width = width;
        }

        void IConfiguration.Resolve(RFASMCompiler compiler)
        {
            compiler.meta!.DataWidth = _width;
        }
    }
}
