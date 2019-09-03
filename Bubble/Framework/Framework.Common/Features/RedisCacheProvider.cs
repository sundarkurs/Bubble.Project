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

        public RedisCacheProvider()
        {
            var connectionString =
                $"{Configurations.RedisHost},ssl={Configurations.RedisIsSecured},password={Configurations.RedisAccountKey}";

            // Get & initalize the Redis Connection String.
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        }

        public static RedisCacheProvider Instance { get; } = new RedisCacheProvider();

        public void Set<T>(string key, T data)
        {
            // Define your retry strategy: retry 3 times, 1 second apart.  
            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            // Define your retry policy using the retry strategy and the Windows Azure storage  
            // transient fault detection strategy.  
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            // Do some work that may result in a transient fault.  
            try
            {
                // Call a method that uses Windows Azure storage and which may  
                // throw a transient exception.  
                retryPolicy.ExecuteAction(
                    () =>
                    {
                        var database = _connectionMultiplexer.GetDatabase();
                        database.StringSet(key, JsonSerializer.Serialize(data), Configurations.CacheExpiryTimeSpan);
                    });
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, $"{GetType().FullName} / {MethodBase.GetCurrentMethod().Name}");
            }
        }

        public T Set<T>(string key, T data, bool returnIfExists)
        {
            // Define your retry strategy: retry 3 times, 1 second apart.  
            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            // Define your retry policy using the retry strategy and the Windows Azure storage  
            // transient fault detection strategy.  
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            // Do some work that may result in a transient fault.  
            try
            {
                // Call a method that uses Windows Azure storage and which may  
                // throw a transient exception.  
                retryPolicy.ExecuteAction(
                    () =>
                    {
                        var database = _connectionMultiplexer.GetDatabase();
                        database.StringSet(key, JsonSerializer.Serialize(data), Configurations.CacheExpiryTimeSpan);
                    });
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, $"{GetType().FullName} / {MethodBase.GetCurrentMethod().Name}");
            }

            return returnIfExists ? Get<T>(key) : data;
        }

        public T Get<T>(string key)
        {
            var cachedData = default(T);
            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            // Do some work that may result in a transient fault.  
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
            catch (Exception ex)
            {
                //Logger.Error(ex, $"{GetType().FullName} / {MethodBase.GetCurrentMethod().Name}");
            }
            return cachedData;
        }

        public void Remove(string key)
        {
            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);

            // Do some work that may result in a transient fault.  
            try
            {
                retryPolicy.ExecuteAction(() =>
                {
                    var database = _connectionMultiplexer.GetDatabase();
                    database.KeyDelete(key);
                });
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, $"{GetType().FullName} / {MethodBase.GetCurrentMethod().Name}");
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
