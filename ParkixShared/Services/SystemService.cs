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
    public class SystemService : RedisDatabaseService
    {
        /// <summary>
        /// Gets the instance of the configuration service singleton.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SystemService Instance { get; } = new SystemService();

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemService"/> class.
        /// </summary>
        protected SystemService()
        {
        }

        /// <summary>
        /// Gets the sensor record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool GetSensor<T>(string guid, out T value) where T : Sensor
        {
            var result = GetValue<T>(key: "SENSOR_" + guid, value: out value);
            return result;
        }

        /// <summary>
        /// Gets the parking lot record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetParkingLot<T>(string id, out T value) where T : ParkingLot
        {
            var result = GetValue<T>(key: "PARKINGLOT_" + id, value: out value);
            return result;
        }

        /// <summary>
        /// Create the parking lot record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parkinglot"></param>
        protected void PutParkingLot<T>(T parkinglot) where T : ParkingLot
        {
            SetValue(key: "PARKINGLOT_" + parkinglot.LotId, value: parkinglot);

            //update system record.
            var system = GetSystem();
            if (!system.ParkingLots.Contains(parkinglot.LotId))
            {
                system.ParkingLots.Add(parkinglot.LotId);
                PutSystem(system);
            }
        }

        /// <summary>
        /// Gets the system.
        /// </summary>
        /// <returns></returns>
        public ParkixSystem GetSystem()
        {
            var result = GetValue<ParkixSystem>(key: "PARKIX_SYSTEM", value: out var system);
            return system;
        }

        /// <summary>
        /// Puts the system.
        /// </summary>
        /// <param name="system">The system.</param>
        protected void PutSystem(ParkixSystem system)
        {
            SetValue(key: "PARKIX_SYSTEM", value: system);
        }
    }
}
