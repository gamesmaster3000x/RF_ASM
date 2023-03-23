using Crimson.CSharp.Assembly;
using Crimson.CSharp.Grammar.Tokens;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class ReturnCStatement : AbstractCrimsonStatement
    {
        public ReturnCStatement (SimpleValueCToken value)
        {
            Value = value;
        }

        public SimpleValueCToken Value { get; }

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment f = new Fragment(0);
            f.Add(new ReturnBStatement());
            return f;
        }
    }
}