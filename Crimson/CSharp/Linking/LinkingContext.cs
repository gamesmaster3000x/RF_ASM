using Crimson.CSharp.Core;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing;
using Crimson.CSharp.Parsing.Tokens;

namespace Crimson.CSharp.Linking
{
    public class LinkingContext
    {

        public Scope CurrentScope { get; private set; }
        internal Dictionary<string, Scope> Links { get; private set; }
        public Compilation Compilation { get; private set; }

        public LinkingContext (Scope currentScope, Dictionary<string, Scope> links, Compilation compilation)
        {
            CurrentScope = currentScope;
            Links = links;
            Compilation = compilation;
        }

        /// <summary>
        /// Makes a shallow copy of the object. 
        /// 
        /// The references to the Name, CurrentUnitLookupPath and Compilation 
        /// will be EQUAL (in the sense of ==).
        /// 
        /// The values of the links of the input context will be copied into
        /// a NEW dictionary. This means that the old context's links will NOT
        /// be affected by edits to the new context's links.
        /// </summary>
        /// <param name="ctx"></param>
        public LinkingContext (Scope currentScope, LinkingContext ctx)
        {
            CurrentScope = currentScope;

            Compilation = ctx.Compilation;

            Links = new Dictionary<string, Scope>();
            foreach (var link in ctx.Links)
            {
                Links.Add(link.Key, link.Value);
            }
        }

        public bool HasScope (string alias)
        {
            return Links.ContainsKey(alias);
        }

        internal Scope GetScope (string alias)
        {
            if (HasScope(alias))
            {
                return Links[alias];
            }

            throw new LinkingException("No alias '" + alias + "' in " + ToString());
        }

        // ===== NAMING =====

        private Dictionary<string, int> _gvarCounts = new Dictionary<string, int>();
        public FullNameCToken GetUniqueGlobalVariableName (FullNameCToken commonName)
        {
            string name = commonName.ToString();
            if (!_gvarCounts.ContainsKey(name))
                _gvarCounts[name] = 0;
            _gvarCounts[name]++;
            return new FullNameCToken($"gvar_{name}_{_gvarCounts[name] - 1}");
        }

        private Dictionary<string, int> _struCounts = new Dictionary<string, int>();
        public FullNameCToken GetUniqueStructureName (FullNameCToken commonName)
        {
            string name = commonName.ToString();
            if (!_struCounts.ContainsKey(name))
                _struCounts[name] = 0;
            _struCounts[name]++;
            return new FullNameCToken($"stru_{name}_{_struCounts[name] - 1}");
        }

        private Dictionary<string, int> _funcCounts = new Dictionary<string, int>();
        public FullNameCToken GetUniqueFunctionName (FullNameCToken commonName)
        {
            string name = commonName.ToString();
            if (!_funcCounts.ContainsKey(name))
                _funcCounts[name] = 0;
            _funcCounts[name]++;
            return new FullNameCToken($"func_{name}_{_funcCounts[name] - 1}");
        }

        private Dictionary<string, int> _svarCounts = new Dictionary<string, int>();
        public FullNameCToken GetUniqueScopeVariableName (FullNameCToken commonName)
        {
            string name = commonName.ToString();
            if (!_svarCounts.ContainsKey(name))
                _svarCounts[name] = 0;
            _svarCounts[name]++;
            return new FullNameCToken($"svar_{name}_{_svarCounts[name] - 1}");
        }

        // =====  =====

        public override string ToString ()
        {
            return $"LinkingContext (scope:{CurrentScope}; links:[{string.Join(",", Links)}])";
        }
    }
}