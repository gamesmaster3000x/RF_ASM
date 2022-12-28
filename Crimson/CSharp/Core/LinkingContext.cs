using Crimson.CSharp.Exception;

namespace Crimson.CSharp.Core
{
    public class LinkingContext
    {

        private string Name { get; }
        public Dictionary<string, string> Links { get; }

        public LinkingContext(string friendlyName) : this(friendlyName, new Dictionary<string, string>())
        {
        }

        public LinkingContext(string friendlyName, Dictionary<string, string> links)
        {
            Name = friendlyName;
            Links = links;
        }

        internal string GetImportPathByAlias(string alias)
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