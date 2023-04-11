using Crimson.CSharp.Core.CURI;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    public class Cache
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
        public static FileInfo CACHE_ROOT { get; private set; } = Crimson.GetRoamingFile("cache/");
        public static FileInfo INDEX { get; private set; } = Crimson.GetRoamingFile("cache/index.json");

        public static CacheIndex Index { get; private set; }
        public class CacheIndex
        {
            public DateTime CreatedTime { get; set; }
            public Dictionary<string, IndexEntry> Contents { get; set; }
        }
        public class IndexEntry
        {
            public DateTime InstalledTime { get; set; }
            public DateTime ModifiedTime { get; set; }
        }

        static Cache ()
        {
            try
            {
                Index = ReadIndex();
            }
            catch (Exception ex)
            {
                Crimson.Panic("Unable to static-construct cache!", Crimson.PanicCode.CACHE, ex);
                throw;
            }
        }

        private static void CreateIndexIfNotPresent ()
        {
            INDEX.Refresh();

            if (!INDEX.Exists)
            {
                string parent = Path.GetDirectoryName(INDEX.FullName)!;
                _ = Directory.CreateDirectory(parent);
            }

            string contents = "";
            using (FileStream readStream = INDEX.Open(FileMode.OpenOrCreate, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(readStream);
                contents = reader.ReadToEnd();
            }

            if (String.IsNullOrWhiteSpace(contents))
            {
                using (FileStream writeStream = INDEX.Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    Index = new CacheIndex
                    {
                        CreatedTime = DateTime.Now,
                        Contents = new Dictionary<string, IndexEntry>()
                    };
                    JsonSerializer.Serialize(writeStream, Index);
                }
            }

            INDEX.Refresh();
        }

        private static CacheIndex? ReadIndex ()
        {
            CacheIndex? index;
            try
            {
                CreateIndexIfNotPresent();
                using (FileStream stream = INDEX.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    index = JsonSerializer.Deserialize<CacheIndex>(stream);
                }
            }
            catch (Exception ex)
            {
                Crimson.Panic($"Unable to deserialise cache index {INDEX}", Crimson.PanicCode.CACHE_JSON, ex);
                throw;
            }
            return index;
        }

        private static void WriteIndex ()
        {
            try
            {
                CreateIndexIfNotPresent();
                using (FileStream stream = INDEX.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    string data = JsonSerializer.Serialize(Index, new JsonSerializerOptions { WriteIndented = true });
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(data);
                }
            }
            catch (Exception ex)
            {
                Crimson.Panic($"Unable to write to cache index {INDEX}", Crimson.PanicCode.CACHE_JSON, ex);
                throw;
            }
            return;
        }

        private static void Fetch (AbstractCURI curi, bool overwrite)
        {
            try
            {
                string data;
                using (Stream readStream = curi.GetStream())
                {
                    StreamReader reader = new StreamReader(readStream);
                    data = reader.ReadToEnd();
                }
                Add(curi, data);
            }
            catch (Exception ex)
            {
                Crimson.Panic($"Error fetching {curi}", Crimson.PanicCode.CACHE_FETCH, ex);
                throw;
            }
        }

        private static void Add (AbstractCURI curi, string data)
        {
            try
            {
                string path = GetLocalCachePath(curi);
                _ = Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (Stream writeStream = Crimson.GetRoamingFile($"cache/{path}").Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(writeStream);
                    writer.Write(data);
                }

                IndexEntry? entry = Index.Contents!.GetValueOrDefault(path, null) ?? new IndexEntry();
                entry.ModifiedTime = DateTime.Now;

                Index.Contents[path] = entry;
                WriteIndex();
            }
            catch (Exception ex)
            {
                Crimson.Panic($"Error adding {curi}", Crimson.PanicCode.CACHE_ADD, ex);
                throw;
            }
        }

        private static string GetLocalCachePath (AbstractCURI curi)
        {
            return $"{curi.Uri.Scheme}/{curi.Uri.LocalPath}";
        }

        // =========== API ===========
        public static void Clear (ClearMode clearMode)
        {
            throw new NotImplementedException();
        }

        public static void Install (AbstractCURI sourceCURI, bool forceRefreshCache)
        {
            throw new NotImplementedException();
        }

        public static void Refresh (AbstractCURI? sourceCURI, bool all = false)
        {
            if (all)
            {
                LOGGER.Info($"Refreshing all {Index.Contents.Count} indexed CURIs...");
                foreach (var pair in Index.Contents)
                {
                    AbstractCURI? curi = AbstractCURI.Create(pair.Key);
                    Refresh(curi);
                }
                LOGGER.Info("Refresh complete.");
            }
            else if (sourceCURI != null)
            {
                LOGGER.Info($"Refreshing {sourceCURI}...");
                Fetch(sourceCURI, true);
                LOGGER.Info($"Done!");
            }
            else
            {
                LOGGER.Error("'--all' is false and the source CURI is null. Refresh failed.");
            }
        }

        public static void Info (AbstractCURI sourceCURI)
        {
            throw new NotImplementedException();
        }

        public enum ClearMode
        {
            ERASE,
            INDEXED,
            DIRECTORIES
        }
    }
}
