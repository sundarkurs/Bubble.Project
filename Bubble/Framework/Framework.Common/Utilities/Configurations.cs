using System;
using System.Configuration;

namespace Framework.Common.Utilities
{
    public static class Configurations
    {
        public static bool RedisEnabled => Convert.ToBoolean(ConfigurationManager.AppSettings["RedisEnabled"]);

        public static string RedisHost => ConfigurationManager.AppSettings["RedisHost"];

        public static string RedisAccountKey => ConfigurationManager.AppSettings["RedisAccountKey"];

        public static string RedisIsSecured => ConfigurationManager.AppSettings["RedisIsSecured"];

        public static int CacheExpiryInMinutes
        {
            get
            {
                int val;
                return
                    int.TryParse(ConfigurationManager.AppSettings["CacheExpiryInMinutes"], out val) ? val : 30;
            }
        }

        public static TimeSpan CacheExpiryTimeSpan
        {
            get
            {
                int val;
                return
                    TimeSpan.FromMinutes(int.TryParse(ConfigurationManager.AppSettings["CacheExpiryInMinutes"], out val)
                        ? val : 30);
            }
        }

        public static bool ValidateCacheDataSerializable => Convert.ToBoolean(ConfigurationManager.AppSettings["ValidateCacheDataSerializable"]);
    }
}
