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
        public LinkedUnit(): this(new List<Function>(), new List<Structure>(), new List<GlobalVariable>())
        {
        }

        public LinkedUnit(IList<Function> functions, IList<Structure> structures, IList<GlobalVariable> globalVariables)
        {
            Functions = functions;
            Structures = structures;
            GlobalVariables = globalVariables;
        }

        public object EntryFunction { get; internal set; }
        internal IList<Function> Functions { get; set; }
        internal IList<Structure> Structures { get; set; }
        internal IList<GlobalVariable> GlobalVariables { get; set; }

        internal void CopyAllFrom(TranslationUnit unit)
        {
            foreach(var f in unit.Functions)
            {
                f.Value.Name = f.Value.Name;
                Functions.Add(f.Value);
            }
            foreach(var s in unit.Structures)
            {
                s.Value.Identifier = s.Value.Identifier;
                Structures.Add(s.Value);
            }
            foreach(var g in unit.GlobalVariables)
            {
                g.Value.Intern.identifier = g.Value.Intern.identifier;
                GlobalVariables.Add(g.Value);
            }
        }
    }
}
