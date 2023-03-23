using Crimson.CSharp.Assembly;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class BasicCallCStatement : AbstractCrimsonStatement
    {
        public string AssemblyText { get; protected set; }

        public BasicCallCStatement (string assemblyText)
        {
            AssemblyText = assemblyText;
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment f = new Fragment(0);
            f.Add(new ArbitraryBStatement(AssemblyText));
            return f;
        }
        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }
    }
}