using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;
using System.Xml.Linq;

namespace Crimson.CSharp.Statements
{
    public abstract class InternalStatement : CrimsonStatement
    {
        protected InternalStatement() : base(false)
        {
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement($"{GetType()}: " + IsLinked()));
            return f;
        }
    }
}