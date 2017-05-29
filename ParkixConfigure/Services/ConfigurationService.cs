using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Parkix.Shared.Services;
using StackExchange.Redis;
using Parkix.Shared.Entities.Redis;
using Parkix.Shared.Entities.Sensor;
using Parkix.Shared.Helpers;
using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Entities.Configure;

namespace Parkix.Shared.Services
{
    /// <summary>
    /// Service to CRUD time-invariant configuration data.
    /// </summary>
    /// <seealso cref="Parkix.Shared.Services.RedisDatabaseService" />
    public class ConfigurationService : SystemService
    {        
        /// <summary>
        /// Singleton of the Configuration Service.
        /// </summary>
        public static new ConfigurationService Instance = new ConfigurationService();

        
        private ConfigurationService()
        {

        }

        /// <summary>
        /// Puts the sensor record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sensor"></param>
        public void PutSensor<T>(T sensor) where T : Sensor
        {
            SetValue(key: "SENSOR_" + sensor.GUID, value: sensor);

            //update system record.
            var system = GetSystem();
            if (!system.Sensors.Contains(sensor.GUID))
            {
                system.Sensors.Add(sensor.GUID);
                PutSystem(system);
            }
        }

        /// <summary>
        /// Deletes the sensor record.
        /// </summary>
        /// <param name="guid"></param>
        public void DeleteSensor(string guid)
        {
            DeleteValue(guid);

            //update system record.
            var system = GetSystem();
            system.Sensors.Remove(guid);
            PutSystem(system);
        }

        /// <summary>
        /// Deletes the parking lot record.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParkingLot(string id)
        {
            DeleteValue(id);

            //update system record.
            var system = GetSystem();
            system.ParkingLots.Remove(id);
            PutSystem(system);
        }

    }
}
