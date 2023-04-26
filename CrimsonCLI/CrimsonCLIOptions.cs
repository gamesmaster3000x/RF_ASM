
using CommandLine;
using CrimsonCore.Core;
using CrimsonCore.CURI;
using CrimsonCore.Exceptions;

namespace CrimsonCLI
{
    public class CrimsonCLIOptions
    {

        /// <summary>
        /// 
        /// </summary>
        [Verb(name: "compile", isDefault: false, aliases: null, Hidden = false,
            HelpText = "Compile a source file into assembly.")]
        public class Compile
        {
            // Source
            [Option(longName: "source", shortName: 's',
                Required = true,
                HelpText = "Path to the root source file to translate. " +
                "If no file extension given, .crm will be assumed.")]
            public string SourcePath { get; set; }
            public AbstractCURI SourceCURI { get => AbstractCURI.Create(SourcePath!, null); }


            // Target
            [Option(longName: "target", shortName: 't',
                Required = false,
                HelpText = "Path to the desired target location or output file. " +
                "If no file extension provided, will assume .crm.")]
            public string? TargetPath { get; set; }
            public AbstractCURI TargetCURI { get => AbstractCURI.Create(TargetPath!, null); }


            // Native library
            [Option(longName: "native", shortName: 'n',
                Required = false,
                HelpText = "Path to the native Crimson library. " +
                "If not provided, will use a packaged version. " +
                "If provided, but a required file is not found, the file will be created from the packaged library.")]
            public string? NativePath { get; set; }
            public AbstractCURI NativeCURI { get => AbstractCURI.Create(NativePath!, null); }


            // Entry function
            [Option(longName: "entry", shortName: 'e',
                Required = false, Default = "main",
                HelpText = "The name of the function where execution should " +
                "start in the primary source file.")]
            public string EntryFunctionName { get; set; }


            // DumpIntermediates
            [Option(longName: "clean", shortName: 'c',
                Required = false, Default = true,
                HelpText = "Should the compiler clean up its temporary files?")]
            public bool DumpIntermediates { get; set; }


            // DumpIntermediates
            [Option(longName: "datawidth", shortName: 'w',
                Required = true,
                HelpText = "The width of an integer, in bytes.")]
            public int DataWidth { get; set; }


            // Cache
            [Option(longName: "usecache", shortName: 'c',
                SetName = "togglecache",
                Required = false, Default = true,
                HelpText = "The width of an integer, in bytes.")]
            public bool UseCache { get; set; }

            [Option(longName: "refresh", shortName: 'r',
                SetName = "togglecache",
                Required = false, Default = false,
                HelpText = "The width of an integer, in bytes.")]
            public bool ForceRefreshCache { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Verb(name: "install", isDefault: false, aliases: null, Hidden = false,
            HelpText = "Install a source file to the Crimson cache for easy access later.")]
        public class Install
        {
            // Source
            [Option(longName: "source", shortName: 's',
                Required = true,
                HelpText = "URI to the source file to install.")]
            public string? SourcePath { get; set; }
            public AbstractCURI SourceCURI { get => AbstractCURI.Create(SourcePath!, null); }

            //
            [Option(longName: "overwrite", shortName: 'o',
                Required = false, Default = false,
                HelpText = "Overwrite existing versions of this file. Consider using the 'refresh' verb instead.")]
            public bool Overwrite { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Verb(name: "clear", isDefault: false, aliases: null, Hidden = false,
            HelpText = "Clear the source file cache.")]
        public class Clear
        {
            // Source
            [Option(longName: "erase", shortName: 'e',
                Required = false,
                Group = "mode",
                HelpText = "Completely remove the cache directories... It's like it was never there!")]
            public bool Erase { get; set; }

            // 
            [Option(longName: "unindexed", shortName: 'u',
                Required = false,
                Group = "mode",
                HelpText = "Remove directories regardless of contents.")]
            public bool Directories { get; set; }

            // Source
            [Option(longName: "indexed", shortName: 'i',
                Required = false,
                Group = "mode",
                HelpText = "Clean only files listed in the cache index.")]
            public bool Indexed { get; set; }

            public CachedBerryClient.ClearMode ClearMode
            {
                get => (Erase, Indexed, Directories) switch
                {
                    (true, false, false) => CachedBerryClient.ClearMode.ERASE,
                    (false, true, false) => CachedBerryClient.ClearMode.INDEXED,
                    (false, false, true) => CachedBerryClient.ClearMode.UNINDEXED,
                    _ => throw new ClearModeException(this)
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Verb(name: "refresh", isDefault: false, aliases: null, Hidden = false,
            HelpText = "Clear the source file cache.")]
        public class Refresh
        {
            // Source
            [Option(longName: "source", shortName: 's',
                SetName = "options",
                Default = "",
                HelpText = "URI to the source file to refresh.")]
            public string? SourcePath { get; set; }
            public AbstractCURI SourceCURI { get => AbstractCURI.Create(SourcePath!, null); }

            // 
            [Option(longName: "all", shortName: 'a',
                SetName = "options",
                Default = false,
                HelpText = "Refresh all items in the index.")]
            public bool All { get; set; }
        }




        internal class ClearModeException : CrimsonCoreException
        {
            public CrimsonCLIOptions.Clear Clear { get; private set; }

            public ClearModeException (CrimsonCLIOptions.Clear clear) : base(CrimsonCore.CrimsonCore.PanicCode.COMPILE_PARSE_STATEMENT)
            {
                Clear = clear;
            }

            public override IList<string> GetDetailedMessage ()
            {
                List<string> strings = new List<string>()
            {
                $"Illegal ClearMode comination (there can only be 1). ",
                $"Found {(Clear.Erase ? "-e" : "")}" +
                        $" {(Clear.Directories ? "-d" : "")}" +
                        $" {(Clear.Indexed ? "-i" : "")}"

            };
                return strings;
            }
        }
    }
}

