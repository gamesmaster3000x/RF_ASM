using Crimson.CSharp.Core;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing;

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

        public override string ToString ()
        {
            return $"LinkingContext (scope:{CurrentScope}; links:[{string.Join(",", Links)}])";
        }
    }
}