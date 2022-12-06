using Crimson.CSharp.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Reflection
{
    internal class CompilationUnit
    {
        private IList<Import> imports = new List<Import>();
        private IList<Function> functions = new List<Function>();
        private IList<Structure> structures = new List<Structure>();
        private IList<GlobalVariable> globalVariables = new List<GlobalVariable>();

        public void AddImport(Import import)
        {
            imports.Add(import);
        }

        public void AddStatement(CompilationUnitStatement statement)
        {
            if (statement is Function)
            {
                functions.Add((Function)statement);
            }
            else if (statement is GlobalVariable)
            {
                globalVariables.Add((GlobalVariable)statement);
            }
            else if (statement is Structure)
            {
                structures.Add((Structure)statement);
            }
            else
            {
                throw new StatementParseException("The given statement " + statement + " may not be added to a CompilationUnit");
            }

        }
    }
}
