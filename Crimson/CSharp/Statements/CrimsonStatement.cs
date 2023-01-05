using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public abstract class CrimsonStatement : ICrimsonToken
    {
        private bool linked;

        public CrimsonStatement(bool linked)
        {
            this.linked = linked;
        }

        // Abstract
        public abstract void Link(LinkingContext ctx);
        public abstract IList<BasicStatement> GetCrimsonBasic();


        // Concrete
        public bool IsLinked() { return linked; }
        public void SetLinked(bool linked) { this.linked = linked; }
    }
}