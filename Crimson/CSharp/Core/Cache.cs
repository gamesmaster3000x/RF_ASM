using Crimson.CSharp.Core.CURI;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    public class Cache
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
        public static FileInfo CACHE_ROOT { get; private set; } = Crimson.GetRoamingFile("cache/");
        public static FileInfo INDEX { get; private set; } = Crimson.GetRoamingFile("cache/index.json");

        public static CacheIndex Index { get; private set; }
        public record CacheIndex
        {
            public DateTime CreatedTime { get; set; }
            public Dictionary<CacheKey, IndexEntry> Contents { get; set; }
        }
        public record IndexEntry
        {
            public string URI { get; set; }
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


        // ============ INDEX ============


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
                        Contents = new Dictionary<CacheKey, IndexEntry>()
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

        private static readonly object _ioLock = new object();
        private static byte[] ReadCached (CacheKey key)
        {
            lock (_ioLock)
            {
                FileInfo info = GetCachedFileInfo(key);
                using (FileStream fileStream = info.OpenRead())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        private static void WriteCached (CacheKey path, byte[] contents)
        {
            lock (_ioLock)
            {
                FileInfo info = GetCachedFileInfo(path);
                _ = Directory.CreateDirectory(Path.GetDirectoryName(info.FullName)!);
                using (Stream writeStream = info.Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(writeStream);
                    writer.Write(contents);
                }
            }
        }


        // ============ KEYS ============

        [JsonConverter(typeof(CacheKeyJsonConverter))]
        public record CacheKey
        {
            public readonly string LocalPath;
            public CacheKey (string localPath) => LocalPath = localPath;
        }

        private static CacheKey GetCacheKey (AbstractCURI curi)
        {
            return new CacheKey($"{curi.Uri.Scheme}/{curi.Uri.LocalPath}");
        }

        public class CacheKeyJsonConverter : JsonConverter<CacheKey>
        {
            public override CacheKey? Read (ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
            }

            public override void Write (Utf8JsonWriter writer, CacheKey value, JsonSerializerOptions options)
            {
            }
        }

        private static FileInfo GetCachedFileInfo (CacheKey key)
        {
            return Crimson.GetRoamingFile($"cache/{key.LocalPath}");
        }

        // =========== API ===========

        public record GetCachedQueryResult
        {
            public readonly bool Exists = false;
            public readonly CacheKey? CacheKey;
            public readonly string? Contents = "";

            public GetCachedQueryResult (bool exists, CacheKey? localPath = null, string? contents = "")
            {
                Exists = exists;
                CacheKey = localPath;
                Contents = contents;
            }
        }

        public static GetCachedQueryResult Get (AbstractCURI curi)
        {
            CacheKey key = GetCacheKey(curi);

            if (!Index.Contents.ContainsKey(key))
                return new GetCachedQueryResult(exists: false);

            FileInfo info = GetCachedFileInfo(key);

            using (Stream stream = info.OpenRead())
            {
                StreamReader reader = new StreamReader(stream);
                string contents = reader.ReadToEnd();
                return new GetCachedQueryResult(exists: true, localPath: key, contents: contents);
            }
        }

        public static GetCachedQueryResult GetOrInstall (AbstractCURI curi)
        {
            GetCachedQueryResult result = Get(curi);
            if (result.Exists) return result;

            Install(curi, true);
            return Get(curi);
        }

        public static void Clear (ClearMode clearMode)
        {
            throw new NotImplementedException();
        }

        public static void Install (AbstractCURI sourceCURI, bool overwrite = false)
        {
            GetCachedQueryResult getResult = Get(sourceCURI);
            if (!overwrite && getResult.Exists)
            {
                LOGGER.Info($"{sourceCURI} is already installed and the 'overwrite' flag is not specified.");
                return;
            }

            if (getResult.Exists) { }

            CacheKey key = GetCacheKey(sourceCURI);
            using (Stream curiStream = sourceCURI.GetStream())
            using (MemoryStream memStream = new MemoryStream())
            {
                curiStream.CopyTo(memStream);
                byte[] contents = memStream.ToArray();

                WriteCached(key, contents);
            }

            Index.Contents[key] = new IndexEntry()
            {
                InstalledTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                URI = sourceCURI.ToString(),
            };
            WriteIndex();
        }

        public static void Refresh (AbstractCURI? sourceCURI, bool all = false)
        {
            if (all)
            {
                LOGGER.Info($"Refreshing all {Index.Contents.Count} indexed CURIs...");
                foreach (var pair in Index.Contents)
                {
                    AbstractCURI? curi = AbstractCURI.Create(pair.Value.URI);
                    Refresh(curi);
                }
                LOGGER.Info("Refresh complete.");
            }
            else if (sourceCURI != null)
            {
                LOGGER.Info($"Refreshing {sourceCURI}...");
                Install(sourceCURI);
                LOGGER.Info($"Done!");
            }
            else
            {
                LOGGER.Error("'--all' is false and the source CURI is null. Refresh failed.");
            }
        }

        public enum ClearMode
        {
            ERASE,
            INDEXED,
            DIRECTORIES
        }
    }
}
