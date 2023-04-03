using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Exceptions
{
    internal class LinkingException : ArgumentException
    {
        public LinkingException (string message) : base(message) { }

        public LinkingException (string message, Exception cause) : base(message, cause) { }
    }

    internal class OtherLinkingException : ArgumentException
    {
        public OtherLinkingException (string message) : base(message) { }

        public OtherLinkingException (string message, Exception cause) : base(message, cause) { }
    }
}
