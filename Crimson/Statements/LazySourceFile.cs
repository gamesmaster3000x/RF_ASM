using Crimson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Crimson.Statements
{
    /// <summary>
    /// The current compilation. This is what ultimately ends up as assembly code.
    /// 
    /// Names of packages are parsed eagerly. Package objects are generated lazily.
    /// </summary>
    internal class LazySourceFile
    {
        public static Regex NEW_LINES = new Regex(@"\r|\n|\r");
        public static Regex BETWEEN_PARENT_BRACKETS = new Regex(@"(?<={).*(?=})");
        public static Regex COMMENT = new Regex(@"//.+($|\r|\n|\r)");
        public static Regex PRECEEDING_AND_TRAILING_SPACES = new Regex(@"^ +(?=.)|(?<! ) +$");

        private static NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        public CrimsonCmdArguments Options { get; }
        private string Path { get; set; }
        private Dictionary<string, LazyPackage> Packages { get; set; }

        public LazySourceFile(string path, CrimsonCmdArguments options)
        {
            LOGGER.Debug("Creating LazySourceFile for " + path);
            Options = options;
            Path = path;
            Packages = GenerateLazyPackages();
        }

        public Dictionary<string, LazyPackage> GenerateLazyPackages()
        {
            if (!File.Exists(Path))
            {
                throw new FileNotFoundException("Cannot find file to compile: " + Path);
            }

            List<string> allText = File.ReadAllLines(Path).ToList();
            string cleanText = SanitizeString(allText);

            // Temporary file
            string cleanPath = Path + ".clean";
            File.WriteAllText(cleanPath, cleanText);
            LOGGER.Debug("Created temporary file: " + cleanPath);
            Cleaner.AddFile(cleanPath);

            // Parse 
            Dictionary<string, string> results = new Dictionary<string, string>();
            List<char> key = new List<char>();
            List<char> value = new List<char>();
            int bracketCount = 0;
            for (int i = 0; i < cleanText.Length;  i++)
            {
                char c = cleanText.ToCharArray()[i];

                // Either starting new value or continuing a current one
                if (c == '{')
                {
                    bracketCount++;
                    continue;
                }
                // Either closing a value 
                if (c == '}')
                {
                    bracketCount--;

                    if (bracketCount < 0)
                    {
                        LOGGER.Fatal("ERROR AT: " + cleanText.Substring(Math.Max(0, i - 100), Math.Min(cleanText.Length - i - 1, 200))); // Out of range :(
                        throw new IndexOutOfRangeException("Too many closing brackets! Bracket count underflow at cleaned character index: " + i);
                    }

                    // C
                    if (bracketCount == 0)
                    {
                        // Store key and value
                        results.Add(new string(key.ToArray()), new string(value.ToArray()));
                        key = new List<char>();
                        value = new List<char>();
                    }
                    continue;
                }
                if (bracketCount == 0)
                {
                    key.Add(c);

                }
                else if (bracketCount > 0)
                {
                    value.Add(c);
                } else
                {
                    throw new IndexOutOfRangeException("Improper bracket count at character index " + i + " of " + Path);
                }
            }

            // G
            Dictionary<string, LazyPackage> packages = new Dictionary<string, LazyPackage>();
            foreach (string r in results.Keys)
            {
                string v = results[r];
                LazyPackage p = new LazyPackage(v, r);
                packages.Add(p.Name, p);
            }

            return packages;
        }

        public string SanitizeString(List<string> textIn)
        {
            StringBuilder builder = new StringBuilder();
            foreach(string s in textIn)
            {
                string value = s;
                value = COMMENT.Replace(value, "");
                value = PRECEEDING_AND_TRAILING_SPACES.Replace(value, "");

                builder.Append(value);
            }

            return builder.ToString();
        }
    }
}
