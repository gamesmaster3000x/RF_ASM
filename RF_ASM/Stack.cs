using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF_ASM
{
    internal class Stack
    {
        //The max size of the stack
        int capacity;

        //The index of the previously added item.
        int back;

        byte[] data;

        public Stack(int capacity)
        {
            this.capacity = capacity;
            data = new byte[capacity];
            back = 0;
        }

        public void Push(byte b)
        {
            data[++back] = b;
        }

        public int Pop
        {
            get
            {
                return data[back--];
            }
        }

        public int Peek
        {
            get { return data[back];}
        }
    }
}
