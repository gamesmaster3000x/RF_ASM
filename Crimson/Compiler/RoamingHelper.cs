namespace Compiler
{
    internal static class RoamingHelper
    {

        public static DirectoryInfo GetRoamingDirectory (string path)
        {
            string betterPath = GetRoamingPath(path);
            return new DirectoryInfo(betterPath);
        }

        public static FileInfo GetRoamingFile (string path)
        {
            string betterPath = GetRoamingPath(path);
            return new FileInfo(betterPath);
        }

        //

        public static string GetRoamingPath (string path)
        {
            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string relative = $"Crimson/{path}";
            string combined = Path.Combine(roaming, relative);
            string full = Path.GetFullPath(combined);
            return full;
        }
    }
}