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
    public class CompilationUnit
    {
        private Dictionary<string, Import> imports = new Dictionary<string, Import>();
        private Dictionary<string, Function> functions = new Dictionary<string, Function>();
        private Dictionary<string, Structure> structures = new Dictionary<string, Structure>();
        private Dictionary<string, GlobalVariable> globalVariables = new Dictionary<string, GlobalVariable>();

        public Dictionary<string, Import> Imports { get => imports; set => imports = value; }
        public Dictionary<string, Function> Functions { get => functions; set => functions = value; }
        public Dictionary<string, Structure> Structures { get => structures; set => structures = value; }
        public Dictionary<string, GlobalVariable> GlobalVariables { get => globalVariables; set => globalVariables = value; }

        public void AddImport(Import import)
        {
            Imports.Add(import.Alias, import);
        }

        public void AddStatement(GlobalStatement statement)
        {
            if (statement is Function)
            {
                Function f = (Function)statement;
                Functions.Add(f.Name, f);
            }
            else if (statement is GlobalVariable)
            {
                GlobalVariable v = (GlobalVariable)statement;
                GlobalVariables.Add(v.Intern.identifier, v);
            }
            else if (statement is Structure)
            {
                Structure s = (Structure)statement;
                Structures.Add(s.Identifier, s);
            }
            else
            {
                throw new StatementParseException("The given statement " + statement + " may not be added to a CompilationUnit");
            }

        }
    }
}
