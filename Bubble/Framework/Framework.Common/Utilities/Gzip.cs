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
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string DecompressJson(string compressedJson)
        {
            if (string.IsNullOrEmpty(compressedJson))
            {
                return compressedJson;
            }
            var bytes = Convert.FromBase64String(compressedJson);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
