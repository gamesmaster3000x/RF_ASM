using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.CURI
{
    public interface ICURIFactory
    {
        /// <summary>
        ///     <para>
        ///         For example: Given the URI http://example.com/program.crm had an import which used the
        ///         relative CURI relative://utilities.crm, to load the import, one should
        ///     </para>
        ///     
        ///     <para>
        ///         A note on relativity: Consider a Crimson project which contains the files:
        ///         <code>
        ///         // Local files
        ///         C:/
        ///         | Programming/
        ///         | | program.crm
        ///         
        ///         // Remote files
        ///         example.com/
        ///         | Crimson/
        ///         | | utilities.crm
        ///         | | maths.crm
        ///         </code>
        ///         
        ///         These files contain the code:
        ///         <code>
        ///         // C:/Programming/program.crm
        ///         // Pull in a remote file as an import
        ///         #using "http://example.com/Crimson/utilities.crm" as utilities
        ///         
        ///         // example.com/Crimson/utilities.crm
        ///         // The remote file declares its own dependencies
        ///         #using "relative://maths.crm" as maths
        ///         
        ///         // example.com/Crimson/maths.crm
        ///         // This file has no imports
        ///         </code>
        ///         
        ///         <para>
        ///             The remote file <b>utilities.crm</b> contains a relative import. 
        ///             To generate a CURI for this file, one should give the relative address '<b>relative://maths.crm</b>' as the <paramref name="relativeOrAbsoluteUri"/>
        ///             and the path of the calling file '<b>http://example.com/Crimson/utilities.crm</b>' as the <paramref name="anchor"/>. 
        ///             This would generate the CURI '<b>http://example.com/Crimson/maths.crm</b>', or an equivalent, depending on the schemes of the URI/CURI and
        ///             the implementation of the <see cref="ICURIFactory"/>.
        ///         </para>
        ///         General rules (dependent on <see cref="ICURIFactory"/> implementation):
        ///         <list type="bullet">
        ///             <item>When the <paramref name="relativeOrAbsoluteUri"/> is absolute, the <paramref name="anchor"/> may be ignored.</item>
        ///         </list>
        ///     </para>
        /// </summary>
        /// <param name="relativeOrAbsoluteUri">The URI to create a CURI from. For example http://example.com/file.txt</param>
        /// <param name="anchor"> In case the URI path is relative, this is the anchor point which it should be relative to.</param>
        /// <returns></returns>
        AbstractCURI Make (Uri relativeOrAbsoluteUri, AbstractCURI? anchor);
    }
}
