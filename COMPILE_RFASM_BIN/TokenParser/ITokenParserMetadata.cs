using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.TokenParser
{
    internal interface ITokenParserMetadata
    {
        string GetValue(string key);
    }
}
