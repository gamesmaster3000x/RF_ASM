using Crimson.CSharp.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core.CURI
{
    /// <summary>
    /// <para>
    ///     <b>Crimson URI</b>
    /// </para>
    /// <para>
    ///     See <see cref="Create(string)"/> and <see cref="Create(Uri)"/>
    /// </para>
    /// <para>
    ///     A wrapper class for a Uri which conceals all of the strange things which Crimson 
    ///     does in the background (i.e. custom "root.crimson" and "native.crimson" hosts).
    /// </para>
    /// </summary>
    public abstract class AbstractCURI : IEquatable<AbstractCURI>
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// <para>
        ///     Get a <see cref="Stream"/> of the resource described by this <see cref="AbstractCURI"/>
        /// </para>
        /// <para>
        ///     For example, a CURI describing "http://example.com/crimson/heap.crm" would get a stream of the bytes of the file "heap.crm"
        /// </para>
        /// </summary>
        /// <returns></returns>
        public abstract Stream GetStream ();

        /// <summary>
        /// <para>
        ///     Used to compare CURIs in <see cref="Library"/>.
        /// </para>
        /// <para>
        ///     Override of the generic Equals method, derived from <see cref="IEquatable{T}"/>.
        /// </para>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool Equals (AbstractCURI? other);

        public virtual Uri Uri { get; set; }

        protected AbstractCURI (Uri uri) => Uri = uri;

        /// <summary>
        /// <para>
        ///     Contains the factories used for making CURIs. 
        ///     The relevant factory is chosen based on the given URI's scheme (e.g. "http" in "http://example.com")
        /// </para>
        /// <para>
        ///     Maps a given scheme to an ICURIFactory, for example: <code>"http" = new HttpCURI.Factory()</code>.
        /// </para>
        /// </summary>
        public static Dictionary<string, ICURIFactory> Factories { get; private set; } = new Dictionary<string, ICURIFactory>()
        {
            { HttpCURI.SCHEME, new HttpCURI.Factory() },
            { FileAbsoluteCURI.SCHEME, new FileAbsoluteCURI.Factory() },
            { NativeCURI.SCHEME, new NativeCURI.Factory() },
            { RelativeCURI.SCHEME, new RelativeCURI.Factory() }
        };

        public static AbstractCURI? Create (string uriText)
        {
            if (String.IsNullOrWhiteSpace(uriText))
                return null;

            string trimmedText = uriText.Trim(' ', '\t', '\n', '\v', '\f', '\r', '"');

            if (Uri.TryCreate(trimmedText, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = false }, out Uri? uri))
                return Create(uri);

            throw new UriFormatException($"Unable to parse illegal URI string '{trimmedText}' ({uriText})");
        }

        public static AbstractCURI Create (Uri uri)
        {
            if (Factories.TryGetValue(uri.Scheme, out ICURIFactory? factory))
            {
                return factory!.Make(uri);
            }

            throw new UriFormatException($"No CURI factory is registered for URIs with scheme {uri.Host}: {uri}");
        }

        public override string ToString ()
        {
            return Uri.ToString();
        }
    }
}
