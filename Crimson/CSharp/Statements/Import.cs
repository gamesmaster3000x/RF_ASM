namespace Crimson.CSharp.Statements
{
    public class Import
    {
        public string Path { get; set; }
        public string Alias { get; set; }

        public Import(string path, string alias)
        {
            Path = path;
            Alias = alias;
        }
    }
}