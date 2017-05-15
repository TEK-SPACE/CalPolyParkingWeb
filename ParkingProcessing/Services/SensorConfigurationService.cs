using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingProcessing.Entities;
using ParkingProcessing.Entities.Configuration;
using ParkingProcessing.Entities.Sensor;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Manages configuration requests for custom sensors.
    /// </summary>
    public class SensorConfigurationService : RedisDatabaseService
    {
        /// <summary>
        /// The instance of the Configuration Service instance.
        /// </summary>
        public static SensorConfigurationService Instance = new SensorConfigurationService();

        /// <summary>
        /// Initialize the instance.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            await base.Initialize(EnvironmentalService.ConfigurationDatabase);
        }

        /// <summary>
        /// Service a configuration polling request.
        /// </summary>
        /// <param name="SensorId"></param>
        /// <returns></returns>
        public ProcessingResponse ServicePassiveConfigurationPolling(string SensorId)
        {
            var config = GetSensorConfiguration(SensorId);

            var response = new ProcessingResponse()
            {
                RequireConfig = config.LastUpdated > config.LastAccessed,
                Config = config.Coordinates
            };

            return response;
        }

        /// <summary>
        /// Gets the lot configuration.
        /// </summary>
        /// <param name="SensorId">The parking lot.</param>
        /// <returns></returns>
        public SensorConfiguration GetSensorConfiguration(string SensorId)
        {
            try
            {
                //get config
                var databaseValue = GetValueForKey(SensorId);
                var configuration =
                    JsonConvert.DeserializeObject<SensorConfiguration>(databaseValue);
                //refresh config last accessed date
                configuration.LastAccessed = DateTime.Now;
                var serializedConfigWithStatus = JsonConvert.SerializeObject(configuration);
                SetValueForKey(SensorId, serializedConfigWithStatus);

                //return config
                return configuration;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationService", e);
                PseudoLoggingService.Log("ConfigurationService", "No configuration found for " + SensorId);
            }

            //if no config found, return no config
            return new SensorConfiguration();
        }

        /// <summary>
        /// Sets the lot configuration.
        /// </summary>
        /// <param name="SensorId">The sensor identifier.</param>
        /// <param name="configuration">The configuration.</param>
        public void UpdateSensorConfiguration(string SensorId, List<ParkingSpotConfiguration> configuration)
        {
            var record = new SensorConfiguration()
            {
                LastAccessed = DateTime.Now.AddYears(-100),
                LastUpdated = DateTime.Now,
                ParkingLotId = SensorId,
                Coordinates = configuration,
                SensorId = SensorId
            };

            //serialize and store
            var serializedConfigWithStatus = JsonConvert.SerializeObject(configuration);
            SetValueForKey(SensorId, serializedConfigWithStatus);
        }
    }
}
