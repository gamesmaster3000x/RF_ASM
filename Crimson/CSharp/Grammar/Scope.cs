using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar
{
    /// <summary>
    /// An unlinked collection of statements which is the direct result of the parsing of a Crimson source file. A Linker may be used to convert this into a LinkedUnit.
    /// </summary>
    public class Scope : AbstractCrimsonStatement
    {

        public Scope? Parent { get; set; }
        private bool _linked;
        private string? _path;

        public Scope (Scope? parent) : this(parent, null) { }

        public Scope (string? path) : this(null, path) { }

        private Scope (Scope? parent, string? path)
        {
            Parent = null;
            _linked = false;
            _path = path;

            Delegates = new List<StatementDelegate>();

            Imports = new Dictionary<string, ImportCStatement>();
            OpHandlers = new List<OperationHandlerCStatement>();
            Functions = new Dictionary<string, FunctionCStatement>();
            Structures = new Dictionary<string, StructureCStatement>();
            GlobalVariables = new Dictionary<string, GlobalVariableCStatement>();
        }

        public bool HasParent ()
        {
            return Parent != null;
        }

        public Scope GetParent ()
        {
            return HasParent() ? Parent! : this;
        }

        public Scope GetRoot ()
        {
            Scope parent = GetParent();
            do
            {
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

            void AddNamedIfNotDuplicate<GCS> (Dictionary<string, GCS> d, GCS gcs, string typeNameForError) where GCS : AbstractCrimsonStatement, INamed
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
                Scope? mappingUnit = compilation.Library.LookupUnitByPath(relativePath);
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
            // Partially shallow-copy the old context (links in a lower level should not get carried up to higher levels)
            LinkingContext newContext = new LinkingContext(ctx);

            Dictionary<string, Scope> dictionary = GetLinks(newContext.Compilation);
            foreach (var link in dictionary)
            {
                ctx.Links.Add(link.Key, link.Value);
            }

            foreach (var d in Delegates)
            {
                d.Invoke().Link(ctx);
            }
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment scopeFrag = new Fragment(1);

            foreach (var d in Delegates)
            {
                scopeFrag.Add(d.Invoke().GetCrimsonBasic());
            }

            return scopeFrag;
        }

        public override string ToString ()
        {
            return $"Scope(Imports:{Imports.Count} Functions:{Functions.Count} Structures:{Structures.Count} GlobalVariables{GlobalVariables.Count})";
        }
    }
}
