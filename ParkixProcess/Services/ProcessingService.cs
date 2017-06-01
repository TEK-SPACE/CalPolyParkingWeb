using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Entities.Sensor;
using Parkix.Shared.Helpers;
using Parkix.Shared.Services;
using System;

namespace Parkix.Process.Services
{
    /// <summary>
    /// Processing incoming sensor data from sensors and GE Current APIs
    /// </summary>
    public class ProcessingService : RedisDatabaseService
    {
        /// <summary>
        /// The instance of the processing service.
        /// </summary>
        public static ProcessingService Instance { get; } = new ProcessingService();

        /// <summary>
        /// Prevents a default instance of the <see cref="ProcessingService"/> class from being created.
        /// </summary>
        private ProcessingService()
        {
        }

        /// <summary>
        /// Accepts the snapshot.
        /// </summary>
        /// <param name="snapshot">The snapshot.</param>
        public bool AcceptSnapshot(ParkingLotSnapshot snapshot)
        {
            var sensorExists = SystemService.Instance.GetSensor<Sensor>(guid: snapshot.SensorId, value: out var sensor);

            if (!sensorExists)
            {
                PseudoLoggingService.Log("Processing Service", "Sensor does not exist.");
                return false;
            }
            
            var frame = GetOrCreateFrame(sensor.TrackingEntity, snapshot.Timestamp);
            frame.UpdateWithSnapshot(snapshot);
            SaveFrame(frame, sensor.TrackingEntity);

            //update newest frame key
            SystemService.Instance.GetParkingLot<ParkingLot>(sensor.TrackingEntity, out var lot);
            lot.NewestKey = sensor.TrackingEntity + "_" + DateHelpers.DatetimeToEpochMs(snapshot.Timestamp.Date);
            SystemService.Instance.PutParkingLot(lot);

            return true;
        }

        /// <summary>
        /// Gets the frame for the specified lot and timestamp.
        /// Creates a new frame if not found in db.
        /// </summary>
        /// <param name="lotid">The lotid.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        private ParkingLotFrame GetOrCreateFrame(string lotid, DateTime timestamp)
        {
            var frameKey = lotid + "_" + DateHelpers.DatetimeToEpochMs(timestamp.Date);
            var exists = GetValue<ParkingLotFrame>(key: frameKey, value: out var frame);

            if (!exists)
            {
                PseudoLoggingService.Log("Processing Service", "New frame created for timestamp");
                frame = new ParkingLotFrame(timestamp.Date);
            }

            return frame;
        }

        /// <summary>
        /// Saves the frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <param name="lotid">The lotid.</param>
        private void SaveFrame(ParkingLotFrame frame, string lotid)
        { 
            var frameKey = lotid + "_" + DateHelpers.DatetimeToEpochMs(frame.StartDateStamp.Date);
            PseudoLoggingService.Log("Processing Service", "Saving frame for key" + frameKey);
            SetValue(key: frameKey, value: frame);
        }
    }
}
