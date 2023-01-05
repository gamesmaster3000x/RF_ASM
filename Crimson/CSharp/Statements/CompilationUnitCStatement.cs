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
                if (functions.ContainsKey(f.Name)) throw new StatementParseException($"Duplicate GlobalStatement name {f.Name} for statement {statement} in unit {this}");
                Functions.Add(f.Name, f);
            }
            else if (statement is GlobalVariable)
            {
                GlobalVariable v = (GlobalVariable)statement;
                if (functions.ContainsKey(v.Name)) throw new StatementParseException($"Duplicate GlobalStatement name {v.Name} for statement {statement} in unit {this}");
                GlobalVariables.Add(v.Name, v);
            }
            else if (statement is Structure)
            {
                Structure s = (Structure)statement;
                if (functions.ContainsKey(s.Name)) throw new StatementParseException($"Duplicate GlobalStatement name {s.Name} for statement {statement} in unit {this}");
                Structures.Add(s.Name, s);
            }
            else
            {
                throw new StatementParseException("The given statement " + statement + " may not be added to a CompilationUnit");
            }

        }

        public override string ToString()
        {
            return $"CompilationUnit(Imports:{Imports.Count} Functions:{Functions.Count} Structures:{Structures.Count} GlobalVariables{GlobalVariables.Count})";
        }
    }
}
