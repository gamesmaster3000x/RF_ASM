using CrimsonCore.CURI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrimsonCore.Core
{
    public interface IScopeProvider
    {

        GetResult Get (AbstractCURI curi);

        public record GetResult
        {
            public readonly bool Exists = false;
            public readonly CacheKey? CacheKey;
            public readonly char[]? Contents;

            public GetResult (bool exists, CacheKey? localPath = null, char[]? contents = null)
            {
                Exists = exists;
                CacheKey = localPath;
                Contents = contents;
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

        public static CacheKey GetCacheKey (AbstractCURI curi)
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

        public static FileInfo GetCachedFileInfo (CacheKey key)
        {
            return CrimsonCore.GetRoamingFile($"cache/{key.LocalPath}");
        }
    }
}
