using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxVM
{
    internal class Stack
    {
        //The max size of the stack
        int capacity;

        //The index of the previously added item.
        int back;

        Word[] data;

        public Stack(int capacity)
        {
            this.capacity = capacity;
            data = new Word[capacity];
            back = 0;
        }

        public void Push(Word b)
        {
            data[++back] = b;
        }

        public Word Pop
        {
            get
            {
                return data[back--];
            }
        }

        public Word Peek
        {
            get { return data[back];}
        }
    }
}
