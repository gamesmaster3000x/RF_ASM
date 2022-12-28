using Crimson.CSharp.Exception;

namespace Crimson.CSharp.Statements
{
    internal class LinkerHelper
    {
        internal static string LinkIdentifier(string identifier, Core.LinkingContext ctx)
        {
            string[] parts = identifier.Split('.');
            if (parts.Length < 1 || parts.Length > 2)
            {
                throw new LinkingException("Identifier must contain only 1 or 2 parts. Found " + parts.Length + " in identifier " + identifier);
            }

            // 
            if (parts.Length == 1)
            {
                return identifier;
            }

           /*
            * Before:
            *  (#using "utils.crm" as u)
            *  u.help
            * 
            * After:
            *  ${utils.crm}.help
            */
            if (parts.Length == 2)
            {
                string alias = parts[0];

                string path = ctx.GetImportPathByAlias(alias);
                string call = parts[1];

                string output = $"{{{path}}}.{call}"; // {{ is used to escape the {, creating ${path}.call in the end
                return output;
            }

            throw new LinkingException("An error occurred while linking. Illegal identifer splitting was not caught? Please report this to the developer.");
        }
    }
}