using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core.CURI
{
    public interface ICURIFactory
    {
        AbstractCURI Make (Uri uri);
    }
}
