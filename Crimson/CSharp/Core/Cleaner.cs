using Antlr4.Runtime.Misc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Crimson.Core
{
    internal class Cleaner
    {
        private NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        private Dictionary<string, FileInfo> _files = new Dictionary<string, FileInfo>();

        string RootPath { get; }

        public Cleaner(string rootPath)
        {
            RootPath = rootPath;
        }

        public FileInfo GetFile(string friendlyName)
        {
            FileInfo info;
            if (!_files.ContainsKey(friendlyName))
            {
                info = new FileInfo(RootPath + friendlyName + ".crm_clnr");
                if (info.Exists)
                {
                    LOGGER.Warn("File " + info + " already exists! (may be overwritten - who needs that one anyway?)");
                }
                _files.Add(friendlyName, info);
            }

            _files.TryGetValue(friendlyName, out info);
            return info;
        }

        public void CleanFiles()
        {
            LOGGER.Info("Cleaning " + _files.Count + " temporary files in " + RootPath);
            foreach(KeyValuePair<string, FileInfo> pair in _files)
            {
                if (pair.Value != null ? pair.Value.Exists : false)
                {
                    LOGGER.Info("Removing temporary file " + pair.Key + ":", pair.Value);
                    pair.Value.Delete();
                } else
                {
                    LOGGER.Warn("Unable to remove temporary file " + pair.Key + ":", pair.Value);
                }
            }
            _files.Clear();

            if (File.Exists(RootPath))
            {
                LOGGER.Info("Removing temporary directory " + RootPath);
                File.Delete(RootPath);
            } else
            {
                LOGGER.Info("Failed to remove temporary directory " + RootPath);
            }
        }
    }
}
