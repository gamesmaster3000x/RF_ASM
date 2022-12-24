using Crimson.CSharp.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// A linked collection of statements whose imports have been resolved (linked) by a Linker.
    /// </summary>
    internal class LinkedUnit
    {
        public LinkedUnit(IList<Function> functions, IList<Structure> structures, IList<GlobalVariable> globalVariables)
        {
            Functions = functions;
            Structures = structures;
            GlobalVariables = globalVariables;
        }

        internal IList<Function> Functions { get; set; }
        internal IList<Structure> Structures { get; set; }
        internal IList<GlobalVariable> GlobalVariables { get; set; }

        internal void CombineWith(LinkedUnit lu)
        {
            foreach(Function function in lu.Functions)
            {
                Functions.Add(function);
            }
            foreach(Structure structure in lu.Structures)
            {
                Structures.Add(structure);
            }
            foreach(GlobalVariable variable in lu.GlobalVariables)
            {
                GlobalVariables.Add(variable);
            }
        }
    }
}
