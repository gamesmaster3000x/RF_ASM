using Crimson.AntlrBuild;
using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Net.Http;

namespace Crimson.CSharp.Grammar.Tokens
{
    public abstract class SimpleValueCToken : ICrimsonToken
    {
        public SimpleValueCToken ()
        {
        }
        public abstract void Link (LinkingContext ctx);
        public abstract string GetText ();
    }
}