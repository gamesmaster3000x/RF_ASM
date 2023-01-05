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

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            return new List<BasicStatement>() { new CommentBStatement("InternalStatement: "  + IsLinked() )};
        }
    }
}