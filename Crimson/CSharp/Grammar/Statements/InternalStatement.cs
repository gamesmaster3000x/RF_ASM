using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Xml.Linq;

namespace Crimson.CSharp.Grammar.Statements
{
    public abstract class InternalStatement : ICrimsonStatement
    {
        private bool _linked = false;

        public virtual Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement($"{GetType()}: linked=" + IsLinked()));
            return f;
        }

        public bool IsLinked()
        {
            return _linked;
        }
        public void SetLinked(bool l)
        {
            _linked = l;
        }
        public abstract void Link(LinkingContext ctx);
    }
}