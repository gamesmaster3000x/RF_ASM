using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Crimson.CSharp.Core
{
    internal class CrimsonCmdArguments
    {
        private string _compilationSourcePath;
        [Option(longName: "source", shortName: 's', Required = true, HelpText = "Path to the source of the root compilation. " +
            "If no file extension given, .rfp will be assumed.")]
        public string CompilationSourcePath
        {
            get { return _compilationSourcePath; }
            set { _compilationSourcePath = Path.GetFullPath(value); }
        }

        private string _compilationTargetPath;
        [Option(longName: "target", shortName: 't', Required = false, HelpText = "Path to the desired target location or output file. " +
            "If no file extension provided, will assume .rfp.")]
        public string CompilationTargetPath
        {
            get { return _compilationTargetPath; }
            set { _compilationTargetPath = Path.GetFullPath(value); }
        }

        private string _nativeLibraryPath;
        [Option(longName: "native", shortName: 'n', Required = false, HelpText = "Path to the native Crimson library. " +
            "If not provided, will use a version packaged with the compiler. " +
            "If provided, but a required file is not found, the file will be created from the packaged library.")]
        public string NativeLibraryPath
        {
            get { return _nativeLibraryPath; }
            set { _nativeLibraryPath = Path.GetFullPath(value); }
        }

        private string _entryFunctionName;
        [Option(longName: "entry", shortName: 'e', Required = false, HelpText = "The name of the function where execution should " +
            "start in the primary source file.", Default = "main")]
        public string EntryFunctionName
        {
            get { return _entryFunctionName; }
            set { _entryFunctionName = value; }
        }

        [Option(longName: "clean", shortName: 'c', Required = false, Default = true, HelpText = "Should the compiler clean up its temporary files?")]
        public bool CleanFiles { get; set; }
    }
}
