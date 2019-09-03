using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Utilities
{
    public static class JsonSerializer
    {

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            //MissingMemberHandling = MissingMemberHandling.Ignore,
            //NullValueHandling = NullValueHandling.Include,
            //DefaultValueHandling = DefaultValueHandling.Include,
            //TypeNameHandling = TypeNameHandling.All,
            //Formatting = Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new DefaultContractResolver()
            {
                IgnoreSerializableInterface = true,
                IgnoreSerializableAttribute = true
            }
        };

        public static string SerializeWithCompression<T>(T model)
        {
            return Gzip.CompressJson(JsonConvert.SerializeObject(model, JsonSerializerSettings));
        }

        public static T DeserializeCompressed<T>(string jsonModel)
        {
            return JsonConvert.DeserializeObject<T>(Gzip.DecompressJson(jsonModel), JsonSerializerSettings);
        }

        public static string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model, JsonSerializerSettings);
        }

        public static T Deserialize<T>(string jsonModel)
        {
            return JsonConvert.DeserializeObject<T>(jsonModel, JsonSerializerSettings);
        }
    }
}
