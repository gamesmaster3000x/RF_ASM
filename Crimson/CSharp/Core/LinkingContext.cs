using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar;

namespace Crimson.CSharp.Core
{
    public class LinkingContext
    {

        private string Name { get; }
        private string CurrentUnitLookupPath { get; }
        internal Dictionary<string, Scope> Links { get; }
        public Compilation Compilation { get; }

        public LinkingContext (string friendlyName, string currentUnitLookupPath, Dictionary<string, Scope> links, Compilation compilation)
        {
            Name = friendlyName;
            CurrentUnitLookupPath = currentUnitLookupPath;
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
        public LinkingContext (LinkingContext ctx)
        {
            Name = ctx.Name;
            CurrentUnitLookupPath = ctx.CurrentUnitLookupPath;
            Links = new Dictionary<string, Scope>();
            Compilation = ctx.Compilation;

            foreach (var link in ctx.Links)
            {
                Links.Add(link.Key, link.Value);
            }
        }

        internal Scope GetUnit (string alias)
        {
            if (Links.ContainsKey(alias))
            {
                return Links[alias];
            }

            throw new LinkingException("No alias '" + alias + "' in " + ToString());
        }

        internal Scope GetCurrentUnit ()
        {
            return GetUnit(CurrentUnitLookupPath);
        }

        public override string ToString ()
        {
            return $"LinkingContext (name:{Name}; path:{CurrentUnitLookupPath}; links:[{String.Join(",", Links)}])";
        }
    }
}