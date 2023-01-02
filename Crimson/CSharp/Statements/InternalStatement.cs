using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    public abstract class InternalStatement : CrimsonStatement
    {
        protected InternalStatement() : base(false)
        {
        }

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>();
        }
    }
}