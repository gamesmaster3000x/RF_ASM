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

        public HeapMemoryAllocatorCStatement? HeapMemoryAllocator { get; private set; }
        public Dictionary<string, ImportCStatement> Imports { get => imports; private set => imports = value; }
        public List<OperationHandlerCStatement> OpHandlers { get => opHandlers; private set => opHandlers = value; }
        public Dictionary<string, FunctionCStatement> Functions { get => functions; private set => functions = value; }
        public Dictionary<string, StructureCStatement> Structures { get => structures; private set => structures = value; }
        public Dictionary<string, GlobalVariableCStatement> GlobalVariables { get => globalVariables; private set => globalVariables = value; }

        public void SetHeapMemoryAllocator(HeapMemoryAllocatorCStatement allocator)
        {
            HeapMemoryAllocator= allocator;
        }

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
            if (statement is FunctionCStatement)
            {
                FunctionCStatement f = (FunctionCStatement)statement;
                if (functions.ContainsKey(f.Name)) throw new StatementParseException($"Duplicate GlobalStatement name {f.Name} for statement {statement} in unit {this}");
                Functions.Add(f.Name, f);
            }
            else if (statement is GlobalVariableCStatement)
            {
                GlobalVariableCStatement v = (GlobalVariableCStatement)statement;
                if (functions.ContainsKey(v.Name)) throw new StatementParseException($"Duplicate GlobalStatement name {v.Name} for statement {statement} in unit {this}");
                GlobalVariables.Add(v.Name, v);
            }
            else if (statement is StructureCStatement)
            {
                StructureCStatement s = (StructureCStatement)statement;
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
