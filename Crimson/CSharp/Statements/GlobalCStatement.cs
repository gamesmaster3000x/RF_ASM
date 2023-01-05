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

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>() { new CommentBStatement("GlobalC:" + Name) };
        }
    }
}