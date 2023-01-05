using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public abstract class GlobalStatement : CrimsonStatement
    {
        public virtual string Name { get; set; }
        protected GlobalStatement() : base(false)
        {
            Name = "";
        }

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>() { new TestBStatement(Name) };
        }
    }
}