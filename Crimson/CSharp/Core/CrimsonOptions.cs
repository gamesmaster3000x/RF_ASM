using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Crimson.CSharp.Core.CURI;
using NLog.Targets;

namespace Crimson.CSharp.Core
{
    public class CrimsonOptions
    {
        // Source
        [Option(longName: "source", shortName: 's', Required = true, HelpText = "Path to the root source file to translate. " +
            "If no file extension given, .crm will be assumed.")]
        public string? SourcePath { get; set; }
        public AbstractCURI SourceCURI { get => AbstractCURI.Create(SourcePath!); }


        // Target
        [Option(longName: "target", shortName: 't', Required = false, HelpText = "Path to the desired target location or output file. " +
            "If no file extension provided, will assume .crm.")]
        public string? TargetPath { get; set; }
        public AbstractCURI TargetCURI { get => AbstractCURI.Create(TargetPath!); }


        // Native library
        [Option(longName: "native", shortName: 'n', Required = false, HelpText = "Path to the native Crimson library. " +
            "If not provided, will use a packaged version. " +
            "If provided, but a required file is not found, the file will be created from the packaged library.")]
        public string? NativePath { get; set; }
        public AbstractCURI NativeCURI { get => AbstractCURI.Create(NativePath!); }


        // Entry function
        [Option(longName: "entry", shortName: 'e', Required = false, HelpText = "The name of the function where execution should " +
            "start in the primary source file.", Default = "main")]
        public string? EntryFunctionName { get; set; }


        // DumpIntermediates
        [Option(longName: "clean", shortName: 'c', Required = false, Default = true, HelpText = "Should the compiler clean up its temporary files?")]
        public bool DumpIntermediates { get; set; }


        // DumpIntermediates
        [Option(longName: "datawidth", shortName: 'w', Required = true, HelpText = "The width of an integer, in bytes.")]
        public int DataWidth { get; set; }
    }
}
