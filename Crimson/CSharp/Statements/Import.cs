namespace Crimson.CSharp.Statements
{
    internal class Import
    {
        string Path { get; set; }
        private string Alias { get; set; }

        public Import(string path, string alias)
        {
            Path = path;
            Alias = alias;
        }
    }
}