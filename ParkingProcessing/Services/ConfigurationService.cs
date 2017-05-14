using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingProcessing.Entities;
using ParkingProcessing.Entities.Configuration;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Manages configuration requests for custom sensors.
    /// </summary>
    public class ConfigurationService
    {
        /// <summary>
        /// The instance of the Configuration Service instance.
        /// </summary>
        public static ConfigurationService Instance = new ConfigurationService();

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigurationService"/> class from being created.
        /// </summary>
        private ConfigurationService()
        {
            
        }

        /// <summary>
        /// Service a configuration polling request.
        /// </summary>
        /// <param name="SensorId"></param>
        /// <returns></returns>
        public ProcessingResponse ServicePassiveConfigurationPolling(string SensorId)
        {
            var config = GetLotConfiguration(SensorId);

            var response = new ProcessingResponse()
            {
                RequireConfig = config.Item1,
                Config = config.Item1 ? config.Item2 : new List<ParkingSpotConfiguration>()
            };

            return response;
        }

        /// <summary>
        /// Gets the lot configuration.
        /// </summary>
        /// <param name="SensorId">The parking lot.</param>
        /// <returns></returns>
        private Tuple<bool, List<ParkingSpotConfiguration>> GetLotConfiguration(string SensorId)
        {
            try
            {
                //get config
                var databaseValue = KeyValueDatabaseService.Instance.GetValueForKey(SensorId);
                var configWithStatus =
                    JsonConvert.DeserializeObject<Tuple<bool, List<ParkingSpotConfiguration>>>(databaseValue);

                //sensor doesn't need config update anymore
                LotConfigurationStatusChange(SensorId, false);

                //return config
                return configWithStatus;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationService", e);
                PseudoLoggingService.Log("ConfigurationService", "No configuration found for " + SensorId);
            }

            //if no config found, return no fonfig
            return new Tuple<bool, List<ParkingSpotConfiguration>>(item1: false, item2: null);
            
        }

        /// <summary>
        /// Sets the lot configuration.
        /// </summary>
        /// <param name="SensorId">The sensor identifier.</param>
        /// <param name="configuration">The configuration.</param>
        public void SetLotConfiguration(string SensorId, List<ParkingSpotConfiguration> configuration)
        {
            //create config obj
            var configWithStatus = new Tuple<bool, List<ParkingSpotConfiguration>>(item1: true, item2: configuration);

            //serialize and store
            var serializedConfigWithStatus = JsonConvert.SerializeObject(configWithStatus);
            KeyValueDatabaseService.Instance.SetValueForKey(SensorId, serializedConfigWithStatus);
        }

        /// <summary>
        /// Update the configuration status of a lot.
        /// </summary>
        /// <param name="SensorId"></param>
        /// <param name="status"></param>
        private void LotConfigurationStatusChange(string SensorId, bool status)
        {
            //get the value from the db
            var databaseValue = KeyValueDatabaseService.Instance.GetValueForKey(SensorId);

            //deserialize and update with new status
            var configWithStatus =
                JsonConvert.DeserializeObject<Tuple<bool, List<ParkingSpotConfiguration>>>(databaseValue);
            configWithStatus = new Tuple<bool, List<ParkingSpotConfiguration>>(item1: status, item2: configWithStatus.Item2);

            //serialize and save in db
            databaseValue = JsonConvert.SerializeObject(configWithStatus);
            KeyValueDatabaseService.Instance.SetValueForKey(SensorId, databaseValue);
        }
    }
}
