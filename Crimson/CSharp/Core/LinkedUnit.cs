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

        internal void CombineWith(string alias, LinkedUnit lu)
        {
            foreach(Function function in lu.Functions)
            {
                function.Name = alias + "." + function.Name;
                Functions.Add(function);
            }
            foreach(Structure structure in lu.Structures)
            {
                structure.Identifier = alias + "." + structure.Identifier;
                Structures.Add(structure);
            }
            foreach(GlobalVariable variable in lu.GlobalVariables)
            {
                variable.Intern.identifier = alias + "." + variable.Intern.identifier;
                GlobalVariables.Add(variable);
            }
        }
    }
}
