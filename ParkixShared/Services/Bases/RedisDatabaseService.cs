using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;
using Parkix.Shared.Services;
using Newtonsoft.Json;
using Parkix.Shared.Entities.Redis;
using Parkix.Shared.Helpers;

namespace Parkix.Shared.Services
{
    /// <summary>
    /// Key Value Database Servicev
    /// </summary>
    public class RedisDatabaseService
    {
        private ConnectionMultiplexer _redis;

        /// <summary>
        /// Initialize the Redis Database Service.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize(RedisService service)
        {
            var creds = service.Credentials;
            Int32.TryParse(service.Credentials.Port, out var port);

            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                {
                    { service.Credentials.Host, port }
                },
                DefaultVersion = new Version(2, 8, 21),
                Password = service.Credentials.Password
            };

            _redis = await ConnectionMultiplexer.ConnectAsync(config);

            PseudoLoggingService.Log(service.Name, "DB connection status: " + _redis.IsConnected.ToString());
        }

        /// <summary>
        /// Gets the value for the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool GetValue<T>(string key, out T value)
        {
            PseudoLoggingService.Log("RedisDatabaseService", "Getting key: " + key);

            IDatabase db = _redis.GetDatabase();
            var data = db.StringGet(key: key);

            if (data.HasValue)
            {
                value = JsonConvert.DeserializeObject<T>(data);
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }

        /// <summary>
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void SetValue(string key, object value)
        {
            PseudoLoggingService.Log("RedisDatabaseService", "Setting key: " + key);

            IDatabase db = _redis.GetDatabase();
            var data = JsonConvert.SerializeObject(value);
            db.StringSet(key: key, value: data);
        }

        /// <summary>
        /// Deletes the value for the specified key.
        /// </summary>
        /// <param name="key"></param>
        protected void DeleteValue(string key)
        {
            IDatabase db = _redis.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
