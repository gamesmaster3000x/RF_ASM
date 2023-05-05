using CommandLine;

namespace RedFoxAssembly.Core
{
    public class RFASMOptions
    {

        private int _dataWidth;
        [Option(longName: "dataWidth", shortName: 'w', Required = false, HelpText = "The width of a 'word' (normally equivalent to an integer) in the program. This may be overwritten by the program itself.")]
        public int DataWidth
        {
            get { return _dataWidth; }
            set { _dataWidth = value; }
        }

        private string _sourcePath;
        [Option(longName: "source", shortName: 's', Required = true, HelpText = "Path to the desired source location or input file.")]
        public string SourcePath
        {
            get { return _sourcePath; }
            set { _sourcePath = Path.GetFullPath(value); }
        }

        private string _metadataPath;
        [Option(longName: "metapath", shortName: 'm', Required = true, HelpText = "Path to the metadata JSON file.")]
        public string MetadataPath
        {
            get { return _metadataPath; }
            set { _metadataPath = Path.GetFullPath(value); }
        }
    }
}