using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar
{
    /// <summary>
    /// An unlinked collection of statements which is the direct result of the parsing of a Crimson source file. A Linker may be used to convert this into a LinkedUnit.
    /// </summary>
    public class Scope: ICrimsonStatement
    {

        public Scope? Parent { get; set; }
        private bool _linked;
        private string? _path;

        public Scope (Scope? parent): this(parent, null){}

        public Scope(string? path): this(null, path) {}

        private Scope (Scope? parent, string? path)
        {
            Parent = null;
            _linked = false;
            _path = path;

            Imports = new Dictionary<string, ImportCStatement>();
            OpHandlers = new List<OperationHandlerCStatement>();
            Functions = new Dictionary<string, FunctionCStatement>();
            Structures = new Dictionary<string, StructureCStatement>();
            GlobalVariables = new Dictionary<string, GlobalVariableCStatement>();
        }

        public bool HasParent() { 
            return Parent != null; 
        }

        public Scope GetParent() { 
            return HasParent() ? Parent! : this; 
        }

        public Scope GetRoot()
        {
            Scope parent = GetParent();
            do { 
                parent = parent.GetParent();
            } while (parent.HasParent());
            return parent;
        }

        public string GetPath ()
        {
            if (!String.IsNullOrWhiteSpace(_path))
            {
                return _path;
            }

            if (HasParent())
            {
                return GetParent().GetPath();
            }

            throw new NullReferenceException($"Scope tree has no associated path: {FamilyToString()}");
        }

        public string FamilyToString ()
        {
            return $"{(HasParent() ? GetParent().FamilyToString() : "")} -> {this}";
        }

        // Statements
        public Dictionary<string, ImportCStatement> Imports { get; private set; }
        public List<OperationHandlerCStatement> OpHandlers { get; private set; }
        public Dictionary<string, FunctionCStatement> Functions { get; private set; }
        public Dictionary<string, StructureCStatement> Structures { get; private set; }
        public Dictionary<string, GlobalVariableCStatement> GlobalVariables { get; private set; }


        public delegate ICrimsonStatement StatementDelegate ();
        public List<StatementDelegate> Delegates { get; private set; }

        public void AddStatement(ICrimsonStatement statement)
        {

            void AddNamedIfNotDuplicate<GCS> (Dictionary<string, GCS> d, GCS gcs, string typeNameForError) where GCS : INamedStatement
            {
                if (d.ContainsKey(gcs.GetName().ToString())) throw new StatementParseException($"Duplicate GlobalStatement name '{gcs.GetName()}' for statement '{statement}' in unit: {this}");
                string name = gcs.GetName().ToString();
                d.Add(name, gcs);
                Delegates.Add(() => d[name]);
            }

            if (statement is FunctionCStatement f) AddNamedIfNotDuplicate(Functions, f, "Function");
            else if (statement is GlobalVariableCStatement g) AddNamedIfNotDuplicate(GlobalVariables, g, "Global Variable");
            else if (statement is StructureCStatement s) AddNamedIfNotDuplicate(Structures, s, "Structure");
            else Delegates.Add(() => statement);
        }

        // Linking

        public Fragment GetCrimsonBasic ()
        {
            throw new NotImplementedException();
        }

        public bool IsLinked ()
        {
            return _linked;
        }

        public void Link (LinkingContext ctx)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Scope(Imports:{Imports.Count} Functions:{Functions.Count} Structures:{Structures.Count} GlobalVariables{GlobalVariables.Count})";
        }
    }
}
