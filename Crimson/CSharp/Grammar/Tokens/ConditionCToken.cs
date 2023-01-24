using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Grammar.Tokens
{
    internal class ConditionCToken : ICrimsonToken
    {
        private ResolvableValueCToken leftValue;
        private Comparator.Values comparator;
        private ResolvableValueCToken rightValue;

        public ConditionCToken(ResolvableValueCToken leftValue, Comparator.Values comparator, ResolvableValueCToken rightValue)
        {
            this.leftValue = leftValue;
            this.comparator = comparator;
            this.rightValue = rightValue;
        }

        public Fragment GetCrimsonBasic()
        {

            /*
             * if ( func(7) > ( 5 + otherfunc(2) ) )
             * 
             * var rval_0 = func(7)
             * var rval_1 = 5 + otherfunc(2)
             * var rval_2 = cond_0 > cond_1
             * 
             * var rval_0 = func(7)
             * var rval_3 = otherfunc(2)
             * var rval_1 = 5 + cond_2
             * var rval_2 = cond_0 > cond_1
             * 
             */
            Fragment left = leftValue.GetCrimsonBasic();
            Fragment right = rightValue.GetCrimsonBasic();
            Fragment combined = new Fragment(0);
            combined.Add(left);
            combined.Add(right);
            string combinedName = FlattenerHelper.GetUniqueResolvableValueFieldName();
            string combinedValue = $"{left.ResultHolder} {Comparator.ToString(comparator)} {right.ResultHolder}";
            combined.Add(new SetBStatement(combinedName, combinedValue));
            combined.ResultHolder = combinedName;
            return combined;
        }

        public void Link(LinkingContext ctx)
        {
            leftValue.Link(ctx);
            rightValue.Link(ctx);
        }
    }
}