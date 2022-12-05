﻿using System;
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

        Value[] data;

        public Stack(int capacity)
        {
            this.capacity = capacity;
            data = new Value[capacity];
            back = 0;
        }

        public void Push(Value b)
        {
            data[++back] = b;
        }

        public Value Pop
        {
            get
            {
                return data[back--];
            }
        }

        public Value Peek
        {
            get { return data[back];}
        }
    }
}
