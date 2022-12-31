using Crimson.CSharp.Exception;

namespace Crimson.CSharp.Statements
{
    internal class LinkerHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier">For example: "console.write"</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        /// <exception cref="LinkingException"></exception>
        internal static Function LinkFunctionCall(string identifier, Core.LinkingContext ctx)
        {
            string[] parts = identifier.Split('.');
            if (parts.Length < 1 || parts.Length > 2)
            {
                throw new LinkingException("FunctionCall identifier must contain only 1 or 2 parts. Found " + parts.Length + " in identifier " + identifier);
            }

            /*
             * Before:
             *  help()
             * 
             * After:
             *  return GetFunction("help")
             */
            if (parts.Length == 1)
            {
                string funcName = parts[0];
                if (!ctx.GetCurrentUnit().Functions.TryGetValue(funcName, out Function? result))
                    throw new LinkingException("Function " + funcName + " does not exist in CompilationUnit " + ctx.GetCurrentUnit() + "; " + ctx.ToString());
                return result;
            }

            /*
             * Before:
             *  (#using "utils.crm" as u)
             *  u.help()
             * 
             * After:
             *  return FunctionCall("C:/utils.crm").GetFunction("help")
             */
            if (parts.Length == 2)
            {
                string alias = parts[0];

                CompilationUnit unit = ctx.GetUnit(alias);
                string funcName = parts[1];

                if (!unit.Functions.TryGetValue(funcName, out Function? result))
                    throw new LinkingException("Function " + funcName + " does not exist in CompilationUnit " + ctx.GetCurrentUnit() + "; " + ctx.ToString());

                return result;
            }

            throw new LinkingException("An error occurred while linking. Illegal identifer splitting was not caught? Please report this to the developer.");
        }

        [Obsolete]
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

                CompilationUnit unit = ctx.GetUnit(alias);
                string call = parts[1];

                string output = $"{{{unit}}}.{call}"; // {{ is used to escape the {, creating ${path}.call in the end //TODO LinkerHelper casts Unit to string
                return output;
            }

            throw new LinkingException("An error occurred while linking. Illegal identifer splitting was not caught? Please report this to the developer.");
        }
    }
}