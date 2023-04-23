using Crimson.CURI;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crimson.Core
{
    public class CachedBerryClient
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
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

        static CachedBerryClient ()
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

            if (string.IsNullOrWhiteSpace(contents))
                using (FileStream writeStream = INDEX.Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    Index = new CacheIndex
                    {
                        CreatedTime = DateTime.Now,
                        Contents = new Dictionary<CacheKey, IndexEntry>()
                    };
                    JsonSerializer.Serialize(writeStream, Index);
                }

            INDEX.Refresh();
        }

        private static readonly object _indexLock = new object();
        private static CacheIndex? ReadIndex ()
        {
            lock (_indexLock)
            {
                CacheIndex? index;
                try
                {
                    CreateIndexIfNotPresent();
                    using (FileStream stream = INDEX.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        index = JsonSerializer.Deserialize<CacheIndex>(stream);
                }
                catch (Exception ex)
                {
                    Crimson.Panic($"Unable to deserialise cache index {INDEX}", Crimson.PanicCode.CACHE_JSON, ex);
                    throw;
                }
                return index;
            }
        }

        private static void WriteIndex ()
        {
            lock (_indexLock)
                try
                {
                    LOGGER.Debug($"Writing to index {INDEX}");
                    CreateIndexIfNotPresent();
                    string data = JsonSerializer.Serialize(Index, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(INDEX.FullName, data);
                    return;
                }
                catch (Exception ex)
                {
                    Crimson.Panic($"Unable to write to cache index {INDEX}", Crimson.PanicCode.CACHE_JSON, ex);
                    throw;
                }
        }

        private static readonly object _ioLock = new object();
        private static char[] ReadCached (CacheKey key)
        {
            lock (_ioLock)
            {
                FileInfo info = GetCachedFileInfo(key);
                using (FileStream fileStream = info.OpenRead())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    byte[] bytes = memoryStream.ToArray();
                    char[] chars = Encoding.UTF8.GetString(bytes).ToCharArray();
                    return chars;
                }
            }
        }

        private static void WriteCached (CacheKey key, byte[] contents)
        {
            lock (_ioLock)
            {
                FileInfo info = GetCachedFileInfo(key);
                _ = Directory.CreateDirectory(Path.GetDirectoryName(info.FullName)!);
                File.WriteAllBytes(info.FullName, contents);
            }
        }


        // ============ KEYS ============

        [JsonConverter(typeof(CacheKeyJsonConverter))]
        [TypeConverter(typeof(CacheKey))]
        public record CacheKey
        {
            public readonly string LocalPath;
            public CacheKey (string localPath) => LocalPath = localPath;
            public override string ToString () => LocalPath;
        }

        private static CacheKey GetCacheKey (AbstractCURI curi)
        {
            return new CacheKey($"{curi.Uri.Scheme}{curi.Uri.LocalPath}");
        }

        public class CacheKeyJsonConverter : JsonConverter<CacheKey>
        {
            public override bool CanConvert (Type typeToConvert)
            {
                bool good = typeof(CacheKey).Equals(typeToConvert);
                return good;
            }

            public override CacheKey ReadAsPropertyName (ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return Read(ref reader, typeToConvert, options)!;
            }

            public override void WriteAsPropertyName (Utf8JsonWriter writer, CacheKey value, JsonSerializerOptions options)
            {
                string? str = value.LocalPath;
                if (string.IsNullOrWhiteSpace(str)) throw new JsonException("Cannot write NullOrWhiteSpace CacheKey.");
                writer.WritePropertyName(str);
            }

            public override CacheKey? Read (ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string? str = reader.GetString();
                if (string.IsNullOrWhiteSpace(str)) throw new JsonException("Cannot read NullOrWhiteSpace CacheKey.");
                return new CacheKey(str!);
            }

            public override void Write (Utf8JsonWriter writer, CacheKey value, JsonSerializerOptions options)
            {
                string? str = value.LocalPath;
                if (string.IsNullOrWhiteSpace(str)) throw new JsonException("Cannot write NullOrWhiteSpace CacheKey.");
                writer.WriteStringValue(str);
            }
        }

        private static FileInfo GetCachedFileInfo (CacheKey key)
        {
            return Crimson.GetRoamingFile($"cache/{key.LocalPath}");
        }

        // Clearing

        private static void Erase ()
        {
            Crimson.GetRoamingDirectory("cache/").Delete(true);
        }

        private static void ClearUnindexed ()
        {

        }

        private static void ClearIndexed ()
        {
            foreach (var x in Index.Contents)
            {
                FileInfo info = GetCachedFileInfo(x.Key);
                info.Delete();
                CutOffEmptyDirectories(info.Directory!);
            }
        }

        private static void CutOffEmptyDirectories (DirectoryInfo root)
        {
            if (root.GetFiles().Length == 0 && root.GetDirectories().Length == 0)
                root.Delete();
        }

        private static void RemoveEmptyDirectories (DirectoryInfo root)
        {
            foreach (DirectoryInfo directory in root.GetDirectories())
            {
                RemoveEmptyDirectories(directory);
                if (root.GetFiles().Length == 0 && root.GetDirectories().Length == 0)
                    directory.Delete(false);
            }
        }

        // =========== API ===========

        public record GetCachedQueryResult
        {
            public readonly bool Exists = false;
            public readonly CacheKey? CacheKey;
            public readonly char[]? Contents;

            public GetCachedQueryResult (bool exists, CacheKey? localPath = null, char[]? contents = null)
            {
                Exists = exists;
                CacheKey = localPath;
                Contents = contents;
            }
        }

        public static GetCachedQueryResult Get (AbstractCURI curi)
        {
            //
            //
            // TODO GET/REFRESHING NOTES
            //
            // ABSTRACT CURI ALWAYS REFRESH.
            // File/absolute always true.
            // Perhaps add to query? http://example.com/file.crm?volatile=true
            //
            //

            CacheKey key = GetCacheKey(curi);

            if (!Index.Contents.ContainsKey(key))
                return new GetCachedQueryResult(exists: false);

            char[] contents = ReadCached(key);
            return new GetCachedQueryResult(exists: true, localPath: key, contents: contents);
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
            switch (clearMode)
            {
                case ClearMode.ERASE:
                    Erase();
                    break;
                case ClearMode.INDEXED:
                    ClearIndexed();
                    break;
                case ClearMode.UNINDEXED:
                    ClearUnindexed();
                    break;
            }
        }

        public enum ClearMode
        {
            ERASE,
            INDEXED,
            UNINDEXED
        }

        public static void Install (AbstractCURI sourceCURI, bool overwrite = false)
        {
            GetCachedQueryResult getResult = Get(sourceCURI);
            if (!overwrite && getResult.Exists)
            {
                LOGGER.Info($"{sourceCURI} is already installed and the 'overwrite' flag is not specified.");
                return;
            }

            LOGGER.Info($"Installing '{sourceCURI.ToShortString()}'...");

            CacheKey key = GetCacheKey(sourceCURI);
            using (Stream curiStream = sourceCURI.GetStream())
            using (MemoryStream memStream = new MemoryStream())
            {
                curiStream.CopyTo(memStream);
                byte[] contents = memStream.ToArray();

                WriteCached(key, contents);
            }

            LOGGER.Debug($"Indexing '{sourceCURI.ToShortString()}' as '{key}'...");
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
                    AbstractCURI? curi = AbstractCURI.Create(pair.Value.URI, null);
                    Refresh(curi);
                }
                LOGGER.Info("Refresh complete.");
            }
            else if (sourceCURI != null)
            {
                LOGGER.Info($"Refreshing '{sourceCURI.ToShortString()}'...");
                Install(sourceCURI, true);
                LOGGER.Info($"Done!");
            }
            else
                LOGGER.Error("'--all' is false and the source CURI is null. Refresh failed.");
        }
    }
}
