using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Framework.Common.Utilities
{
    public static class Gzip
    {
        public static string CompressJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return json;
            }

            var bytes = Encoding.Unicode.GetBytes(json);
            using (var inputStream = new MemoryStream(bytes))
            using (var outputStream = new MemoryStream())
            {
                using (var gStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    inputStream.CopyTo(gStream);
                }

                return Convert.ToBase64String(outputStream.ToArray());
            }
        }

        public static string DecompressJson(string compressedJson)
        {
            if (string.IsNullOrEmpty(compressedJson))
            {
                return compressedJson;
            }

            var bytes = Convert.FromBase64String(compressedJson);
            using (var inputStream = new MemoryStream(bytes))
            using (var outputStream = new MemoryStream())
            {
                using (var gStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    gStream.CopyTo(outputStream);
                }

                return Encoding.Unicode.GetString(outputStream.ToArray());
            }
        }
    }
}
