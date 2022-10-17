using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.Core
{
    internal class Cleaner
    {
        private static NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        private static List<string> _files = new List<string>();

        public static void AddFile(string file)
        {
            LOGGER.Debug("Added " + file + " to list for cleaning");
            _files.Add(file);
        }

        public static void CleanFiles()
        {
            foreach(string file in _files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
