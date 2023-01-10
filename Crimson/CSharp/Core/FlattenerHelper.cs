using Crimson.CSharp.Exception;
using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    internal class FlattenerHelper
    {
        private static int branchNameCounter = 0;
        public static string GetUniqueBranchName()
        {
            return $"branch_{branchNameCounter++}";
        }

        private static int resolvableValueFieldNameCounter = 0;
        public static string GetUniqueResolvableValueFieldName()
        {
            return $"rval_{resolvableValueFieldNameCounter++}";
        }
    }
}