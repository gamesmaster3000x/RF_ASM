using CrimsonCore.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Compiler.Core;

namespace Compiler.CURI
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
        ///     Directly get the remote <see cref="Stream"/> of the resource described by this <see cref="AbstractCURI"/>.
        ///     Typically it is preferable to use <see cref="CachedBerryClient.Get(AbstractCURI)"/> as that will return a cached 
        ///     version if it exists which may reduce latency due to, for example, internet speeds.
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

        public string ToShortString ()
        {
            List<string> parts = new List<string>();

            if (Uri.Segments.Length <= 5)
                parts.AddRange(Uri.Segments);
            else
            {
                parts.AddRange(Uri.Segments.Take(3));
                parts.Add(".../");
                parts.AddRange(Uri.Segments.TakeLast(2));
            }

            return $"{Uri.Scheme}://{Uri.UserInfo}@{Uri.Host}:{Uri.Port}{string.Join("", parts)}";
        }

        public override string ToString ()
        {
            return Uri.ToString();
        }

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

        public static AbstractCURI? Create (string relativeOrAbsoluteUri, AbstractCURI? anchor)
        {
            if (string.IsNullOrWhiteSpace(relativeOrAbsoluteUri))
                return null;

            string trimmedText = relativeOrAbsoluteUri.Trim(' ', '\t', '\n', '\v', '\f', '\r', '"');

            if (Uri.TryCreate(trimmedText, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = false }, out Uri? uri))
                return Create(uri, anchor);

            throw new UriFormatException($"Unable to parse illegal URI string '{trimmedText}' ({relativeOrAbsoluteUri})");
        }

        public static AbstractCURI Create (Uri relativeOrAbsoluteUri, AbstractCURI? anchor)
        {
            if (Factories.TryGetValue(relativeOrAbsoluteUri.Scheme, out ICURIFactory? factory))
                return factory!.Make(relativeOrAbsoluteUri, anchor);

            throw new UriFormatException($"No CURI factory is registered for URIs with scheme {relativeOrAbsoluteUri.Host}: {relativeOrAbsoluteUri}");
        }
    }
}
