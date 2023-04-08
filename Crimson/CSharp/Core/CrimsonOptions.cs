using System;
using System.Collections.Generic;
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
        public AbstractCURI SourceUri { get; private set; }
        public void SetSourcePath (string path) => SourceUri = AbstractCURI.Create(path);
        public void SetSourcePath (AbstractCURI uri) => SourceUri = uri;


        // Target
        [Option(longName: "target", shortName: 't', Required = false, HelpText = "Path to the desired target location or output file. " +
            "If no file extension provided, will assume .crm.")]
        public AbstractCURI TargetUri { get; private set; }
        public void SetTargetPath (string path) => TargetUri = AbstractCURI.Create(path);
        public void SetTargetPath (AbstractCURI uri) => TargetUri = uri;


        // Native library
        [Option(longName: "native", shortName: 'n', Required = false, HelpText = "Path to the native Crimson library. " +
            "If not provided, will use a packaged version. " +
            "If provided, but a required file is not found, the file will be created from the packaged library.")]
        public AbstractCURI NativeUri { get; private set; }
        public void SetNativePath (string path) => NativeUri = AbstractCURI.Create(path);
        public void SetNativePath (AbstractCURI uri) => NativeUri = uri;


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


        // DumpIntermediates
        [Option(longName: "datawidth", shortName: 'w', Required = true, HelpText = "The width of an integer, in bytes.")]
        public int DataWidth { get; set; }


        // Targeted language
        [Option(Group = "platform")] public bool CrimsonBasic { get; set; }
        [Option(Group = "platform")] public bool RFASM { get; set; }

    }
}
