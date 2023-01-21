using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM.Components
{
    internal class Terminal
    {
        public readonly byte lane;

        public void Clock()
        {
            Fetch();
            Execute();
        }

        public void Fetch()
        {

        }

        public void Execute()
        {

        }
    }
}
