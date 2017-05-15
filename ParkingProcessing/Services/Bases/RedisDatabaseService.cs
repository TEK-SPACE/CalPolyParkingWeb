using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ParkingProcessing.Entities.Redis;
using StackExchange.Redis;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Key Value Database Servicev
    /// </summary>
    public class RedisDatabaseService
    {

        protected ConnectionMultiplexer _redis;

        /// <summary>
        /// Initialize the Key Value Database Service.
        /// </summary>
        /// <returns></returns>
        protected async Task Initialize(RedisService service)
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
        /// Gets the value for key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetValueForKey(string key)
        {
            IDatabase db = _redis.GetDatabase();
            var result = db.StringGet(key);

            if (!result.HasValue)
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        /// <summary>
        /// Sets the value for key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValueForKey(string key, string value)
        {
            IDatabase db = _redis.GetDatabase();
            db.StringSet(key: key, value: value);
        }
    }
}
