using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN
{
    /// <summary>
    /// Class responsible for the pre-processing of non-compiled data. This inlcludes:
    /// <list type="number">
    ///     <item>Resolving compiler directives (.val, .jump)</item>
    ///     <item>Removing comments</item>
    /// </list>
    /// </summary>
    internal class Parser
    {
        public static Dictionary<string, Data> constants = new Dictionary<string, Data>();

        public static List<string> Parse(List<string> input)
        {
            List<string> output = new List<string>(); 

            for(int i = 0; i < input.Count; i++)
            {

            }

            return output;
        }
    }
}
