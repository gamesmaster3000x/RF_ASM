using Crimson.CSharp.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Statements
{
    /// <summary>
    /// An unlinked collection of statements which is the direct result of the parsing of a Crimson source file. A Linker may be used to convert this into a LinkedUnit.
    /// </summary>
    internal class CompilationUnit
    {
        private IList<Import> imports = new List<Import>();
        private IList<Function> functions = new List<Function>();
        private IList<Structure> structures = new List<Structure>();
        private IList<GlobalVariable> globalVariables = new List<GlobalVariable>();

        internal IList<Import> Imports { get => imports; set => imports = value; }
        internal IList<Function> Functions { get => functions; set => functions = value; }
        internal IList<Structure> Structures { get => structures; set => structures = value; }
        internal IList<GlobalVariable> GlobalVariables { get => globalVariables; set => globalVariables = value; }

        public void AddImport(Import import)
        {
            Imports.Add(import);
        }

        public void AddStatement(GlobalStatement statement)
        {
            if (statement is Function)
            {
                Functions.Add((Function)statement);
            }
            else if (statement is GlobalVariable)
            {
                GlobalVariables.Add((GlobalVariable)statement);
            }
            else if (statement is Structure)
            {
                Structures.Add((Structure)statement);
            }
            else
            {
                throw new StatementParseException("The given statement " + statement + " may not be added to a CompilationUnit");
            }

        }
    }
}
