using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public abstract class GlobalCStatement : CrimsonStatement
    {
        public virtual string Name { get; set; }
        protected GlobalCStatement() : base(false)
        {
            Name = "";
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement("GlobalC:" + Name));
            return f;
        }
    }
}