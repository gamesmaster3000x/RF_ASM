using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar;

namespace Crimson.CSharp.Core
{
    public class LinkingContext
    {

        private string Name { get; }
        private string CurrentUnitLookupPath { get; }
        internal Dictionary<string, Scope> Links { get; }

        public LinkingContext(string friendlyName, string currentUnitLookupPath, Dictionary<string, Scope> links)
        {
            Name = friendlyName;
            CurrentUnitLookupPath = currentUnitLookupPath;
            Links = links;
        }

        internal Scope GetUnit(string alias)
        {
            if (Links.ContainsKey(alias))
            {
                return Links[alias];
            }

            throw new LinkingException("No alias '" + alias + "' in " + ToString());
        }

        internal Scope GetCurrentUnit()
        {
            return GetUnit(CurrentUnitLookupPath);
        }

        public override string ToString()
        {
            return "LinkingContext (" + Name + ") {" + String.Join(",", Links) + "}";
        }
    }
}