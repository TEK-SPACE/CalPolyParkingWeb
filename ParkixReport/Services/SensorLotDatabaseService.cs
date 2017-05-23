using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Parkix.Process.Entities.Sensor;
using StackExchange.Redis;

namespace Parkix.Process.Services
{
    public class SensorLotDatabaseService : RedisDatabaseService
    {
        public static SensorLotDatabaseService Instance { get; } = new SensorLotDatabaseService();


        private SensorLotDatabaseService()
        {
            
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            await base.Initialize(EnvironmentalService.SensorLotDatabase);
        }

        /// <summary>
        /// Gets all sensor lot mapping records.
        /// </summary>
        /// <returns></returns>
        public List<SensorLotMappingRecord> GetAllSensorLotMappingRecords()
        {
            var db = _redis.GetDatabase();
            var server = _redis.GetServer(db.IdentifyEndpoint());

            var records = new List<SensorLotMappingRecord>();
            foreach (RedisKey key in server.Keys())
            {
                records.Add(GetSensorLotMappingRecord(key.ToString()));
            }

            return records;
        }

        public SensorLotMappingRecord GetSensorLotMappingRecord(string sensorid)
        {
            try
            {
                var result = GetStringForKey	(sensorid);
                var record = JsonConvert.DeserializeObject<SensorLotMappingRecord>(result);
                return record;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("SensorLotDatabaseService", "No value for key" + sensorid);
            }
            
            return new SensorLotMappingRecord();

        }

        public void SetSensorLotMappingRecord(SensorLotMappingRecord record)
        {
            var data = JsonConvert.SerializeObject(record);
            SetStringForKey(record.SensorId, data);
        }
    }
}
