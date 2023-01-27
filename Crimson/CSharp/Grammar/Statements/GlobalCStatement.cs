using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    public abstract class GlobalCStatement : ICrimsonStatement
    {
        public virtual string Name { get; set; } = "";
        public bool _linked = false;

        public virtual Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement("GlobalC:" + Name));
            return f;
        }

        public abstract void Link(LinkingContext ctx);

        public bool IsLinked()
        {
            return _linked;
        }
        public void SetLinked(bool l)
        {
            _linked = l;
        }
    }
}