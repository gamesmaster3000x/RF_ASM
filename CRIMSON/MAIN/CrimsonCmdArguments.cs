using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace CRIMSON.MAIN
{
    internal class CrimsonCmdArguments
    {
        [Option(longName: "source", shortName: 's', Required = true, HelpText = "Path to the source of the root compilation. " +
            "If no file extension given, .rfp will be assumed.")]
        public string CompilationSourcePath { get; set; }


        [Option(longName: "target", shortName: 't', Required = false, HelpText = "Path to the desired target location or output file. " +
            "If no file extension provided, will assume .rfp.")]
        public string CompilationTargetPath { get; set; }


        [Option(longName: "native", shortName: 'n', Required = false, HelpText = "Path to the native Crimson library. " +
            "If not provided, will use a version packaged with the compiler. " +
            "If provided, but a required file is not found, the file will be created from the packaged library.")]
        public string NativeLibraryPath { get; set; }
    }
}
