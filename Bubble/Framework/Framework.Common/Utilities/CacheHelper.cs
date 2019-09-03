using Framework.Common.Exceptions;
using Framework.Common.Features;
using Framework.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Utilities
{
    public static class CacheHelper
    {
        private static readonly ICacheProvider DefaultCacheProvider;

        static CacheHelper()
        {
            try
            {
                //if (Configurations.RedisEnabled)
                //{
                //    DefaultCacheProvider = RedisCacheProvider.Instance;
                //    //Logger.Info("RedisCacheProvider Activated", "Common");
                //}
                //else
                //{
                //    //Logger.Info("InMemoryCacheProvider Activated", "Common");
                //    DefaultCacheProvider = InMemoryCacheProvider.Instance;
                //}

                DefaultCacheProvider = InMemoryCacheProvider.Instance;

                if (DefaultCacheProvider == null)
                {
                    throw new CacheInitializationException("Caching Provider Not Activated.");
                }
            }
            catch (Exception exception)
            {
                //Logger.Info("Exception when Initalizing cache." + exception.Message + " " + exception.InnerException, "CacheHelper");
                //Logger.Info("InMemoryCacheProvider Activated", "Common");

                DefaultCacheProvider = InMemoryCacheProvider.Instance;
            }
        }

        public static T Get<T>(string key)
        {
            return DefaultCacheProvider.Get<T>(key);
        }

        public static void Set<T>(string key, T value)
        {
            DefaultCacheProvider.Set(key, value);
        }

        public static T Set<T>(string key, T value, bool returnIfExists)
        {
            return DefaultCacheProvider.Set(key, value, returnIfExists);
        }

        public static void Remove(string key)
        {
            DefaultCacheProvider.Remove(key);
        }
    }
}
