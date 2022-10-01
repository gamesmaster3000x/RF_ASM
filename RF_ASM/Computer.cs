using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
    internal class Computer
    {
        public byte[] memory;

        //Must be at least 3 elements, which are registers A, B and C.
        public byte[] registers;

        public Stack stack;
    }
}
