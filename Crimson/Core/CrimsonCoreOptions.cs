
using CommandLine;
using CrimsonCore.Exceptions;
using CrimsonCore.CURI;

namespace CrimsonCore.Core
{
    public class CrimsonCoreOptions
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
    }
}

