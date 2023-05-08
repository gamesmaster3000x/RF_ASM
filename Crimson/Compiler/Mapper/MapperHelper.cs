using Compiler.Common.Exceptions;
using Compiler.Parser.Syntax;
using Compiler.Parser.Syntax.Functions;

namespace Compiler.Mapper
{
    internal class MapperHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier">For example: "console.write"</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        /// <exception cref="LinkingException"></exception>
        internal static Function GetLinkedFunctionForCall (FullName identifier, MappingContext ctx)
        {
            if (identifier == null) throw new LinkingException("Cannot link a null identifer");

            /*
             * Before:
             *  (#using "utils.crm" as u)
             *  u.help()
             * 
             * After:
             *  return FunctionCall("C:/utils.crm").GetFunction("help")
             */
            if (identifier.HasLibrary() && identifier.HasMember())
            {
                string alias = identifier.LibraryName;

                Scope scope = ctx.GetScope(alias);
                string funcName = identifier.MemberName;

                Function? func = scope.FindFunction(funcName);
                return func ?? throw new LinkingException($"External Function '{identifier}' does not exist in external {alias}:{scope}; " + ctx.ToString());
            }

            /*
             * Before:
             *  help()
             * 
             * After:
             *  return GetFunction("help")
             */
            if (identifier.HasMember())
            {
                string funcName = identifier.MemberName;
                Function? func = ctx.CurrentScope.FindFunction(funcName);

                return func ?? throw new LinkingException($"Internal Function '{identifier}' does not exist in internal {ctx.CurrentScope} or its parents; " + ctx.ToString());
            }

            // Somehow only has a library name?
            throw new LinkingException($"The idenifier {identifier} with no member name somehow got through the parsing process?");
        }
    }
}