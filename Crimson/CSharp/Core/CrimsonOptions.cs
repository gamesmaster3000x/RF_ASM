using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Crimson.CSharp.Core
{
    public class CrimsonOptions
    {
        // Source
        private string _translationSourcePath;
        [Option(longName: "source", shortName: 's', Required = true, HelpText = "Path to the root source file to translate. " +
            "If no file extension given, .crm will be assumed.")]
        public string TranslationSourcePath
        {
            get { return _translationSourcePath; }
            set { _translationSourcePath = Path.GetFullPath(value); }
        }

        // Target
        private string _translationTargetPath;
        [Option(longName: "target", shortName: 't', Required = false, HelpText = "Path to the desired target location or output file. " +
            "If no file extension provided, will assume .crm.")]
        public string TranslationTargetPath
        {
            get { return _translationTargetPath; }
            set { _translationTargetPath = Path.GetFullPath(value); }
        }

        // Native library
        private string _nativeLibraryPath;
        [Option(longName: "native", shortName: 'n', Required = false, HelpText = "Path to the native Crimson library. " +
            "If not provided, will use a packaged version. " +
            "If provided, but a required file is not found, the file will be created from the packaged library.")]
        public string NativeLibraryPath
        {
            get { return _nativeLibraryPath; }
            set { _nativeLibraryPath = Path.GetFullPath(value); }
        }

        // Entry function
        private string _entryFunctionName;
        [Option(longName: "entry", shortName: 'e', Required = false, HelpText = "The name of the function where execution should " +
            "start in the primary source file.", Default = "main")]
        public string EntryFunctionName
        {
            get { return _entryFunctionName; }
            set { _entryFunctionName = value; }
        }

        // DumpIntermediates
        [Option(longName: "clean", shortName: 'c', Required = false, Default = true, HelpText = "Should the compiler clean up its temporary files?")]
        public bool DumpIntermediates { get; set; }

        // Targeted language
        [Option(Group = "platform")] public bool CrimsonBasic { get; set; }
        [Option(Group = "platform")] public bool RFASM { get; set; }

    }
}
