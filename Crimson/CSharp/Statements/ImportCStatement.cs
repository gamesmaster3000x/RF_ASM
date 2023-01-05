namespace Crimson.CSharp.Statements
{
    public class ImportCStatement
    {
        public string Path { get; set; }
        public string Alias { get; set; }

        public ImportCStatement(string path, string alias)
        {
            Path = path;
            Alias = alias;
        }
    }
}