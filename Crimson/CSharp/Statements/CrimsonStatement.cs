using Crimson.CSharp.Core;

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


        // Concrete
        public bool IsLinked() { return linked; }
        public void SetLinked(bool linked) { this.linked = linked; }
    }
}