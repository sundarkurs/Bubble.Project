using Framework.Common.Utilities;
using Framework.Core.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.AzureStorage;
using Microsoft.Practices.TransientFaultHandling;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Features
{
    internal sealed class RedisCacheProvider : ICacheProvider, IDisposable
    {
        private ConnectionMultiplexer _connectionMultiplexer;

        private bool _disposed;

        private short _retryCount = 3;

        public RedisCacheProvider()
        {
            var connectionString =
                $"{Configurations.RedisHost},ssl={Configurations.RedisIsSecured},password={Configurations.RedisAccountKey}";

            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        }

        public static RedisCacheProvider Instance { get; } = new RedisCacheProvider();

        public void Set<T>(string key, T data)
        {
            var retryStrategy = new Incremental(_retryCount, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            try
            {
                retryPolicy.ExecuteAction(() =>
                {
                    var database = _connectionMultiplexer.GetDatabase();
                    database.StringSet(key, JsonSerializer.Serialize(data), Configurations.CacheExpiryTimeSpan);
                });
            }
            catch (Exception ex) { }
        }

        public T Set<T>(string key, T data, bool returnIfExists)
        {
            var retryStrategy = new Incremental(_retryCount, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            try
            {
                retryPolicy.ExecuteAction(() =>
                {
                    var database = _connectionMultiplexer.GetDatabase();
                    database.StringSet(key, JsonSerializer.Serialize(data), Configurations.CacheExpiryTimeSpan);
                });
            }
            catch (Exception ex) { }

            return returnIfExists ? Get<T>(key) : data;
        }

        public T Get<T>(string key)
        {
            var cachedData = default(T);
            var retryStrategy = new Incremental(_retryCount, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            try
            {
                retryPolicy.ExecuteAction(() =>
                {
                    var database = _connectionMultiplexer.GetDatabase();
                    var data = database.StringGet(key);
                    if (data.HasValue)
                    {
                        cachedData = JsonSerializer.Deserialize<T>(database.StringGet(key));
                    }
                });
            }
            catch (Exception ex) { }

            return cachedData;
        }

        public void Remove(string key)
        {
            var retryStrategy = new Incremental(_retryCount, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            try
            {
                retryPolicy.ExecuteAction(() =>
                {
                    var database = _connectionMultiplexer.GetDatabase();
                    database.KeyDelete(key);
                });
            }
            catch (Exception ex) { }
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
                if (_connectionMultiplexer != null)
                {
                    _connectionMultiplexer.Dispose();
                    _connectionMultiplexer = null;
                    _disposed = true;
                }
            }
        }

        ~RedisCacheProvider()
        {
            Dispose(true);
        }
    }
}
