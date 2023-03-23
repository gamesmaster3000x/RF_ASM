using Crimson.CSharp.Assembly;
using Crimson.CSharp.Linking;

namespace Crimson.CSharp.Grammar.Tokens
{
    internal class ConditionCToken : ICrimsonToken
    {
        public OperationResolvableValueCToken Operation { get; }

        public ConditionCToken (OperationResolvableValueCToken operation)
        {
            Operation = operation;
        }


        public Fragment GetCrimsonBasic ()
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
            /*Fragment left = leftValue.GetBasicFragment();
            Fragment right = rightValue.GetBasicFragment();*/
            Fragment combined = new Fragment(0);
            combined.ResultHolder = "c_r_h";
            /*combined.Add(left);
            combined.Add(right);
            string combinedName = FlattenerHelper.GetUniqueResolvableValueFieldName();
            string combinedValue = $"{left.ResultHolder} {Comparator.ToString(comparator)} {right.ResultHolder}";
            combined.Add(new SetBStatement(combinedName, combinedValue));
            combined.ResultHolder = combinedName;*/
            return combined;
        }

        public void Link (LinkingContext ctx)
        {
            Operation.Link(ctx);
        }
    }
}