using Framework.Common.Utilities;
using Framework.Core.Interfaces;
using System;
using System.Reflection;
using System.Runtime.Caching;

namespace Framework.Common.Features
{
    public sealed class InMemoryCacheProvider : ICacheProvider, IDisposable
    {
        private const int DefaultCacheExpirationTimeinMinutes = 30;
        private readonly CacheItemPolicy _defaultCacheItemPolicy;

        private MemoryCache _cache;
        private bool _disposed;

        private InMemoryCacheProvider()
        {
            _defaultCacheItemPolicy = new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(DefaultCacheExpirationTimeinMinutes),

                // when item got removed from the cache, call back event will dispose the item immediately.
                RemovedCallback = args =>
                {
                    // Check if the items is disposable.
                    var value = args.CacheItem.Value as IDisposable;
                    // dispose the item.
                    value?.Dispose();
                }
            };

            _cache = MemoryCache.Default;
        }

        public static InMemoryCacheProvider Instance { get; } = new InMemoryCacheProvider();

        public void Set<T>(string key, T data)
        {
            data = GetFormattedData(key, data);
            _cache.Set(new CacheItem(key, data), _defaultCacheItemPolicy);
        }

        public T Set<T>(string key, T data, bool returnIfExists)
        {
            if (!_cache.Contains(key))
            {
                data = GetFormattedData(key, data);
                _cache.Set(new CacheItem(key, data), _defaultCacheItemPolicy);
            }

            return returnIfExists ? Get<T>(key) : data;
        }

        private T GetFormattedData<T>(string key, T data)
        {
            // TODO: this function is a temporary measure to verify the data is serializable or not, change the configuration to true, if this required.
            // Check if the data added to cache is serializable, otherwise if not serializable, it may lead to issues when using redis cache.
            if (!Configurations.ValidateCacheDataSerializable)
            {
                return data;
            }

            try
            {
                var serializeData = JsonSerializer.Serialize(data);
                var deserializeData = JsonSerializer.Deserialize<T>(serializeData);
                return deserializeData;
            }
            catch (Exception ex)
            {
                //Logger.Warn($"Serialization failed for Key : { key}", $"{GetType().FullName} / {MethodBase.GetCurrentMethod().Name}");
                //Logger.Error(ex, $"{GetType().FullName} / {MethodBase.GetCurrentMethod().Name}");
            }

            return data;
        }

        public T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public void Remove(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _cache.Remove(key);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_cache != null)
                {
                    _cache.Dispose();
                    _cache = null;
                    _disposed = true;
                }
            }
        }

        ~InMemoryCacheProvider()
        {
            Dispose(true);
        }
    }
}
