using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Grammar
{
    /// <summary>
    /// An unlinked collection of statements which is the direct result of the parsing of a Crimson source file. A Linker may be used to convert this into a LinkedUnit.
    /// </summary>
    public class CompilationUnit
    {
        private Dictionary<string, ImportCStatement> imports = new Dictionary<string, ImportCStatement>();
        private List<OperationHandlerCStatement> opHandlers = new List<OperationHandlerCStatement>();
        private Dictionary<string, FunctionCStatement> functions = new Dictionary<string, FunctionCStatement>();
        private Dictionary<string, StructureCStatement> structures = new Dictionary<string, StructureCStatement>();
        private Dictionary<string, GlobalVariableCStatement> globalVariables = new Dictionary<string, GlobalVariableCStatement>();

        public Dictionary<string, ImportCStatement> Imports { get => imports; private set => imports = value; }
        public List<OperationHandlerCStatement> OpHandlers { get => opHandlers; private set => opHandlers = value; }
        public Dictionary<string, FunctionCStatement> Functions { get => functions; private set => functions = value; }
        public Dictionary<string, StructureCStatement> Structures { get => structures; private set => structures = value; }
        public Dictionary<string, GlobalVariableCStatement> GlobalVariables { get => globalVariables; private set => globalVariables = value; }

        public void AddImport(ImportCStatement import)
        {
            Imports.Add(import.Alias, import);
        }

        public void AddOpHandler(OperationHandlerCStatement handler)
        {
            OpHandlers.Add(handler);
        }

        public void AddStatement(GlobalCStatement statement)
        {

            void CheckContentsAndNameElseAdd<T> (Dictionary<string, T> d, T item, string typeNameForError) where T : GlobalCStatement
            {
                if (String.IsNullOrWhiteSpace(item.Name))
                    throw new StatementParseException($"Cannot give a null or whitespace name to a {typeNameForError}");
                if (d.ContainsKey(item.Name))
                    throw new StatementParseException($"Duplicate GlobalStatement name '{item.Name}' for statement '{statement}' in unit: {this}");
                d.Add(item.Name, item);
            }

            if (statement is FunctionCStatement f) CheckContentsAndNameElseAdd(functions, f, "Function");
            else if (statement is GlobalVariableCStatement g) CheckContentsAndNameElseAdd(globalVariables, g, "Global Variable");
            else if (statement is StructureCStatement s) CheckContentsAndNameElseAdd(structures, s, "Structure");
            else throw new StatementParseException("The given statement " + statement + " may not be added to a CompilationUnit");       
        }

        public override string ToString()
        {
            return $"CompilationUnit(Imports:{Imports.Count} Functions:{Functions.Count} Structures:{Structures.Count} GlobalVariables{GlobalVariables.Count})";
        }
    }
}
