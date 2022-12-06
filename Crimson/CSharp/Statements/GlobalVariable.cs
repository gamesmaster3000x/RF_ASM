using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Reflection
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    internal class GlobalVariable: GlobalStatement
    {

        public string Identifier { get; set; }
        public ResolvableValue Value { get; }

        public GlobalVariable(string rawText, string identifier, ResolvableValue value) : base(rawText)
        {
            Identifier = identifier;
            Value = value;
        }
    }
}
