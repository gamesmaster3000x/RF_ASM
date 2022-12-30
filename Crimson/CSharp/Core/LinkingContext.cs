using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    public class LinkingContext
    {

        private string Name { get; }
        internal Dictionary<string, CompilationUnit> Links { get; }

        public LinkingContext(string friendlyName) : this(friendlyName, new Dictionary<string, CompilationUnit>())
        {
        }

        public LinkingContext(string friendlyName, Dictionary<string, CompilationUnit> links)
        {
            Name = friendlyName;
            Links = links;
        }

        internal CompilationUnit GetUnit(string alias)
        {
            if (Links.ContainsKey(alias))
            {
                return Links[alias];
            }

            throw new LinkingException("No alias '" + alias + "' in " + ToString());
        }

        public override string ToString()
        {
            return "LinkingContext (" + Name + ") {" + String.Join(",", Links) + "}";
        }
    }
}