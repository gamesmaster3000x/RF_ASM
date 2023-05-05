using RedFoxAssembly.Core;
using RedFoxAssembly.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static RedFoxAssembly.Statements.IData;

namespace RedFoxAssembly.Statements
{
    internal interface IData
    {
        public byte[] GetBytes (RFASMCompiler compiler);

        int GetWidth (RFASMCompiler compiler);

        public bool IsTargetingRegister ();

        public string ToString ();

        public static RegisterTarget ParseRegisterTarget (char c)
        {
            char u = char.ToUpper(c);
            switch (u)
            {
                case 'R': return RegisterTarget.REGISTER;
                case 'G': return RegisterTarget.GENERAL_REGISTER;
                case 'S': return RegisterTarget.SPECIALISED_REGISTER;
                case 'C': return RegisterTarget.COMPONENT_REGISTER;
            }

            throw new ParsingException("Cannot parse register prefix " + u);
        }

        public static int GetRegisterOffset (RegisterTarget t)
        {
            return t switch
            {
                RegisterTarget.NONE => 0,
                RegisterTarget.REGISTER => 0,
                RegisterTarget.SPECIALISED_REGISTER => 0,
                RegisterTarget.GENERAL_REGISTER => 128,
                RegisterTarget.COMPONENT_REGISTER => 64,
                _ => throw new ParsingException("Cannot get offset for register target " + t),
            };
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
