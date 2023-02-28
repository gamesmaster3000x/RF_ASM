﻿using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar;
using Crimson.CSharp.Grammar.Statements;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Core
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
        internal static FunctionCStatement LinkFunctionCall(FullNameCToken identifier, LinkingContext ctx)
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

                CompilationUnit unit = ctx.GetUnit(alias);
                string funcName = identifier.MemberName;

                if (!unit.Functions.TryGetValue(funcName, out FunctionCStatement? result))
                {
                    throw new LinkingException("Function '" + funcName + "' does not exist in CompilationUnit " + unit + " via LinkingContext " + ctx.ToString());
                }

                return result;
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
                if (!ctx.GetCurrentUnit().Functions.TryGetValue(funcName, out FunctionCStatement? result))
                    throw new LinkingException("Function " + funcName + " does not exist in CompilationUnit " + ctx.GetCurrentUnit() + "; " + ctx.ToString());
                return result;
            }

            // Somehow only has a library name?
            throw new LinkingException($"The idenifier {identifier} with no member name somehow got through the parsing process?");
        }

        [Obsolete]
        internal static FullNameCToken LinkIdentifier (FullNameCToken identifier, LinkingContext ctx)
        {
            if (identifier == null) throw new LinkingException("Cannot link a null identifer");

            /*
             * Before:
             *  (#using "utils.crm" as u)
             *  u.help
             * 
             * After:
             *  ${utils.crm}.help
             */
            if (identifier.HasLibrary() && identifier.HasMember())
            {
                string alias = identifier.LibraryName;

                CompilationUnit unit = ctx.GetUnit(alias);
                string call = identifier.MemberName;

                FullNameCToken output = new FullNameCToken($"{{{unit}}}", call); // {{ is used to escape the {, creating ${path}.call in the end //TODO LinkerHelper casts Unit to string
                return output;
            }

            // Is a short local name with no library, so no linking needed
            if (identifier.HasMember())
            {
                return new FullNameCToken(null, identifier.MemberName);
            }

            // Somehow only has a library name?
            throw new LinkingException($"The idenifier {identifier} with no member name somehow got through the parsing process?");
        }
    }
}