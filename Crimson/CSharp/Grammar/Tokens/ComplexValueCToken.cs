using Crimson.AntlrBuild;
using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public abstract class ComplexValueCToken : ICrimsonToken
    {

        public ComplexValueCToken()
        {
        }

        public abstract void Link(LinkingContext ctx);
        public abstract Fragment GetBasicFragment();
    }
}