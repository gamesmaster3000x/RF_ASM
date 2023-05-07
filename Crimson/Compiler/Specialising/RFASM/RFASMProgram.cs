using NLog;

namespace Compiler.Specialising.RFASM
{
    internal class RFASMProgram : AbstractSpecificAssemblyProgram
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public List<Fragment> Fragments { get; }

        public RFASMProgram ()
        {
            Fragments = new List<Fragment>();
        }

        public void Add (Fragment f)
        {
            Fragments.Add(f);
        }

        public void Add (params RFASMStatement[] statements)
        {
            Fragment f = new Fragment(0);
            f.Add(statements);
            Add(f);
        }

        public void Add (RFASMStatement statement)
        {
            Fragment f = new Fragment(0);
            f.Add(statement);
            Fragments.Add(f);
        }

        public override IEnumerable<Fragment> GetFragments ()
        {
            return Fragments;
        }

        public override string GetExtension ()
        {
            return ".rfp";
        }

        public override void Write (string path)
        {
            LOGGER.Info("Writing RFASMProgram to " + path);

            List<string> lines = new List<string>();
            foreach (var f in GetFragments())
                lines.AddRange(f.GetLines());

            _ = Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, lines.ToArray());
            LOGGER.Info("Written!");
        }
    }
}