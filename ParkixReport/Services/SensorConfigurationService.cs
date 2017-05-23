using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Parkix.Process.Entities;
using Parkix.Process.Entities.Configuration;
using Parkix.Process.Entities.Sensor;
using Parkix.Process.Helpers;

namespace Parkix.Process.Services
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
        public bool ServicePassiveConfigurationPolling(string SensorId, out ProcessingResponse response)
        {
            var result = GetSensorConfiguration(SensorId, out var configuration);

            if (result)
            {
                response = new ProcessingResponse()
                {
                    RequireConfig = configuration.LastUpdated > configuration.LastAccessed,
                    Config = configuration.Coordinates
                };

                return true;
            }

            response = null;
            return false;
        }

        /// <summary>
        /// Gets the lot configuration.
        /// </summary>
        /// <param name="SensorId">The parking lot.</param>
        /// <returns></returns>
        public bool GetSensorConfiguration(string SensorId, out SensorConfiguration configuration)
        {
            try
            {
                //get config
                var databaseValue = GetStringForKey(SensorId);
                configuration =
                    JsonConvert.DeserializeObject<SensorConfiguration>(databaseValue);
                //refresh config last accessed date
                configuration.LastAccessed = DateTime.Now;
                var serializedConfigWithStatus = JsonConvert.SerializeObject(configuration);
                SetStringForKey(SensorId, serializedConfigWithStatus);

                //return config
                return true;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationService", e);
                PseudoLoggingService.Log("ConfigurationService", "No configuration found for " + SensorId);
            }

            configuration = null;
            return false;
        }

        /// <summary>
        /// Gets the lot configuration.
        /// </summary>
        /// <param name="SensorId">The parking lot.</param>
        /// <returns></returns>
        public bool GetSensorPhoto(string SensorId, out byte[] photoBytes)
        {
            try
            {
                //get config
                var databaseValue = GetStringForKey(SensorId + "_PHOTO");
                photoBytes = JsonConvert.DeserializeObject<byte[]>(databaseValue);

                //return config
                return true;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationService", e);
                PseudoLoggingService.Log("ConfigurationService", "No configuration found for " + SensorId);
            }

            //if no config found, return no config
            photoBytes = null;
            return false;
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
            SetStringForKey(SensorId, serializedConfigWithStatus);
        }

        /// <summary>
        /// Update Sensor Photo.
        /// </summary>
        /// <param name="SensorId"></param>
        /// <param name="photoBytes"></param>
        public void SetSensorPhoto(string SensorId, byte[] photoBytes)
        {
            //serialize and store
            var serializedPhoto = JsonConvert.SerializeObject(photoBytes);
            SetStringForKey(SensorId + "_PHOTO", serializedPhoto);
        }
    }
}
