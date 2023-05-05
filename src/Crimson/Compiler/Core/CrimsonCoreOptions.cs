
using CommandLine;
using Compiler.CURI;

namespace Compiler.Core
{
    public class CrimsonCoreOptions
    {
        // Source
        public string SourcePath { get; set; }
        public AbstractCURI SourceCURI { get => AbstractCURI.Create(SourcePath!, null); }


        // Target
        public string? TargetPath { get; set; }
        public AbstractCURI TargetCURI { get => AbstractCURI.Create(TargetPath!, null); }


        // Native library
        public string? NativePath { get; set; }
        public AbstractCURI NativeCURI { get => AbstractCURI.Create(NativePath!, null); }


        // Entry function
        public string EntryFunctionName { get; set; }


        // DumpIntermediates
        public bool DumpIntermediates { get; set; }


        // DataWidth
        public int DataWidth { get; set; }


        // Cache
        public bool UseCache { get; set; }
        public bool ForceRefreshCache { get; set; }
    }
}

