using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Key Value Database Service
    /// </summary>
    public class KeyValueDatabaseService
    {
        /// <summary>
        /// The instance
        /// </summary>
        public static KeyValueDatabaseService Instance = new KeyValueDatabaseService();

        private ConnectionMultiplexer _redis;

        /// <summary>
        /// Prevents a default instance of the <see cref="KeyValueDatabaseService"/> class from being created.
        /// </summary>
        private KeyValueDatabaseService()
        {
        }

        /// <summary>
        /// Initialize the Key Value Database Service.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            var creds = EnvironmentalService.RedisService.Credentials;
            Int32.TryParse(EnvironmentalService.RedisService.Credentials.Port, out var port);

            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                {
                    { EnvironmentalService.RedisService.Credentials.Host, port }
                },
                DefaultVersion = new Version(2, 8, 21),
                Password = EnvironmentalService.RedisService.Credentials.Password
            };
            

            _redis = await ConnectionMultiplexer.ConnectAsync(config);

            PseudoLoggingService.Log("KeyValueDatabaseService", "DB connection status: " + _redis.IsConnected.ToString());
        }

        /// <summary>
        /// Gets the value for key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetValueForKey(string key)
        {
            IDatabase db = _redis.GetDatabase();
            return db.StringGet(key);
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
