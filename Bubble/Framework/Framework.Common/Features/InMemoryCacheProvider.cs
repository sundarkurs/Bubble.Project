using Framework.Common.Utilities;
using Framework.Core.Interfaces;
using System;
using System.Reflection;
using System.Runtime.Caching;

namespace Framework.Common.Features
{
    public sealed class InMemoryCacheProvider : ICacheProvider, IDisposable
    {
        private readonly int DefaultCacheExpirationTimeinMinutes = Configurations.CacheExpiryInMinutes;
        private readonly CacheItemPolicy _defaultCacheItemPolicy;

        private MemoryCache _cache;
        private bool _disposed;

        private InMemoryCacheProvider()
        {
            _defaultCacheItemPolicy = new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(DefaultCacheExpirationTimeinMinutes),

                RemovedCallback = args =>
                {
                    var value = args.CacheItem.Value as IDisposable;
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
            catch (Exception ex) { }

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
