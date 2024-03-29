﻿using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static RedFoxAssembly.CSharp.Statements.IData;

namespace RedFoxAssembly.CSharp.Statements
{
    internal interface IData
    {
        public byte[] GetBytes(RFASMCompiler compiler);

        int GetWidth(RFASMCompiler compiler);

        public bool IsTargetingRegister();

        public string ToString();

        public static RegisterTarget ParseRegisterTarget(char c)
        {
            char u = Char.ToUpper(c);
            switch (u)
            {
                case 'R': return RegisterTarget.REGISTER;
                case 'G': return RegisterTarget.GENERAL_REGISTER;
                case 'S': return RegisterTarget.SPECIALISED_REGISTER;
                case 'C': return RegisterTarget.COMPONENT_REGISTER;
            }

            throw new ParsingException("Cannot parse register prefix " + u);
        }

        public static int GetRegisterOffset(RegisterTarget t)
        {
            //TODO Get proper register offsets (IData)
            switch (t)
            {
                case RegisterTarget.NONE: return 0;
                case RegisterTarget.REGISTER: return 0;
                case RegisterTarget.SPECIALISED_REGISTER: return 0;
                case RegisterTarget.GENERAL_REGISTER: return 128;
                case RegisterTarget.COMPONENT_REGISTER: return 64;
            }

            throw new ParsingException("Cannot get offset for register target " + t);
        }

        public enum RegisterTarget
        {
            NONE,
            REGISTER,
            SPECIALISED_REGISTER,
            GENERAL_REGISTER,
            COMPONENT_REGISTER
        }
    }
}
