using Crimson.CSharp.Core;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Generalising;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Statements;
using NLog;

namespace Crimson.CSharp.Parsing
{
    /// <summary>
    /// An unlinked collection of statements which is the direct result of the parsing of a Crimson source file. A Linker may be used to convert this into a LinkedUnit.
    /// </summary>
    public class Scope : AbstractCrimsonStatement
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public Scope? Parent { get; set; }
        private string? _path;

        private string _name;
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_name))
                    return Parent == null ? "(root)" : "(anon)";
                return _name;
            }
            set => _name = value;
        }

        public Scope (string name, Scope? parent) : this(name, parent, null) { }

        public Scope (string name, string? path) : this(name, null, path) { }

        private Scope (string name, Scope? parent, string? path)
        {
            Parent = null;
            _path = path;
            Name = name;

            Delegates = new List<StatementDelegate>();

            Imports = new Dictionary<string, ImportCStatement>();
            OpHandlers = new List<OperationHandlerCStatement>();
            Functions = new Dictionary<string, FunctionCStatement>();
            Structures = new Dictionary<string, StructureCStatement>();
            GlobalVariables = new Dictionary<string, GlobalVariableCStatement>();
        }

        public bool HasParent () => Parent != null;
        public Scope GetParent () => HasParent() ? Parent! : this;
        public string FamilyToString () => $"{(HasParent() ? GetParent().FamilyToString() : "(root)")} -> {this}";

        public Scope GetRoot ()
        {
            Scope parent = GetParent();
            do
                parent = parent.GetParent();
            while (parent.HasParent());
            return parent;
        }

        public string GetPath ()
        {
            if (!string.IsNullOrWhiteSpace(_path))
                return _path;

            if (HasParent())
                return GetParent().GetPath();

            throw new NullReferenceException($"Scope tree has no associated path: {FamilyToString()}");
        }

        public FunctionCStatement? FindFunction (string funcName)
        {
            if (Functions.ContainsKey(funcName))
                return Functions[funcName];
            else
                return HasParent() ? GetParent().FindFunction(funcName) : null;
        }

        internal StructureCStatement? FindStructure (string structureName)
        {
            if (Structures.ContainsKey(structureName))
                return Structures[structureName];
            else
                return HasParent() ? GetParent().FindStructure(structureName) : null;
        }

        // Statements
        public Dictionary<string, ImportCStatement> Imports { get; private set; }
        public List<OperationHandlerCStatement> OpHandlers { get; private set; }
        public Dictionary<string, FunctionCStatement> Functions { get; private set; }
        public Dictionary<string, ScopeVariableCStatement> Variables { get; private set; }
        public Dictionary<string, StructureCStatement> Structures { get; private set; }
        public Dictionary<string, GlobalVariableCStatement> GlobalVariables { get; private set; }


        /// <summary>
        /// The "master" list of statements, which contains a list of suppliers 
        /// to the statements in the source file in the order that they appear.
        /// 
        /// This allows different types of statement to be maintained and queried 
        /// seperately through their own maps whilst still maintaining the order 
        /// that they appear in the source.
        /// 
        /// Hence the resulting Crimson Basic will contain all of the correct
        /// statements in the correct order, allowing for arbitrary/overflow
        /// execution and more granular control of execution flow.
        /// </summary>
        public List<StatementDelegate> Delegates { get; private set; }
        public delegate AbstractCrimsonStatement StatementDelegate ();

        public void AddStatement (AbstractCrimsonStatement statement)
        {
            if (statement == null) throw new ArgumentNullException($"Cannot add null statement to scope {this}.");

            void AddNamedIfNotDuplicate<GCS> (Dictionary<string, GCS> d, GCS gcs, string typeNameForError) where GCS : AbstractCrimsonStatement, INamed
            {
                if (d == null) throw new ArgumentNullException($"Cannot pass null {typeof(Dictionary<string, GCS>)} for statement adding.");
                if (gcs == null) throw new ArgumentNullException($"Cannot pass null {typeof(GCS)} for statement adding.");
                if (string.IsNullOrWhiteSpace(typeNameForError)) throw new ArgumentNullException("Cannot pass null or whitespace type name for statement adding.");

                if (d.ContainsKey(gcs.GetName().ToString())) throw new StatementParseException($"Duplicate GlobalStatement name '{gcs.GetName()}' for statement '{statement}' in unit: {this}");
                string name = gcs.GetName().ToString();
                d.Add(name, gcs);
                Delegates.Add(() => d[name]);
            }

            if (statement is IHasScope scopeStatement)
            {
                scopeStatement.GetScope().Parent = this;

                if (statement is INamed namedStatement)
                    scopeStatement.GetScope().Name = namedStatement.GetName().ToString();
            }

            // Special cases
            if (statement is FunctionCStatement f)
                AddNamedIfNotDuplicate(Functions, f, "Function");
            else if (statement is GlobalVariableCStatement g) AddNamedIfNotDuplicate(GlobalVariables, g, "Global Variable");
            else if (statement is StructureCStatement s) AddNamedIfNotDuplicate(Structures, s, "Structure");
            else if (statement is Scope sc)
            {
                sc.Parent = this;
                sc.Name = $"{Name}/{sc.Name}";
                Delegates.Add(() => statement);
            }
            else Delegates.Add(() => statement);
        }

        // Linking

        public Dictionary<string, Scope> GetLinks (Compilation compilation)
        {
            Dictionary<string, Scope> Links = new Dictionary<string, Scope>();
            foreach (KeyValuePair<string, ImportCStatement> importPair in Imports)
            {
                /*
                 * Get the alias of the dependency.
                 * For example:
                 *  The alias of '#using "utils.crm" as u' is 'u'
                 */
                string alias = importPair.Key;

                /*
                 * Get the relative path of the unit which the alias refers to.
                 * For example:
                 *  '#using "utils.crm" as u' may result in 'C:/utils.crm'
                 */
                string relativePath = importPair.Value.Path;

                /*
                 * 
                 */
                Scope? mappingUnit = compilation.Library.LookupScopeByPath(relativePath);
                if (mappingUnit == null) throw new LinkingException("Could not add unloadable unit " + relativePath + " (alias=" + alias + ") to mapping context");
                Links.Add(alias, mappingUnit);
            }
            return Links;
        }

        /// <summary>
        /// For Scope, this is being called when the Scope is within another Scope. This means that it will need to add its own links.
        /// </summary>
        /// <param name="ctx"></param>
        public override void Link (LinkingContext ctx)
        {
            LOGGER.Debug($"Linking Scope: {FamilyToString()}");

            // Partially shallow-copy the old context (links in a lower level should not get carried up to higher levels)
            LinkingContext newContext = new LinkingContext(this, ctx);

            Dictionary<string, Scope> dictionary = GetLinks(newContext.Compilation);
            foreach (var link in dictionary)
                newContext.Links.Add(link.Key, link.Value);

            foreach (var d in Delegates)
            {
                AbstractCrimsonStatement s = d.Invoke();
                if (s == null)
                {
                    _ = d.Invoke();
                    throw new NullReferenceException($"Delegate {d} returned a null statement during linking of {ctx}.");
                }

                s.Link(newContext);
            }
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ScopeAssemblyStructure structure = new ScopeAssemblyStructure();
            context.EnterScope();

            foreach (var d in Delegates)
                structure.AddSubStructure(d.Invoke().Generalise(context));

            context.LeaveScope();
            return structure;
        }

        public override string ToString ()
        {

            return $"Scope({Name}; I:{Imports.Count} F:{Functions.Count} S:{Structures.Count} G:{GlobalVariables.Count})";
        }

        internal ScopeVariableCStatement? FindScopeVariable(string name)
        {
            // TODO Check Scope.FindScopeVariable works
            Scope parent = this;
            ScopeVariableCStatement var = null;
            if(!Variables.TryGetValue(name, out var) && HasParent())
            {
                return GetParent().FindScopeVariable(name);
            }
            return var;
        }
    }
}