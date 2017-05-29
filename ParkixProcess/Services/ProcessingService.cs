using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Entities.Sensor;
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
            var sensorExists = GetValue<Sensor>(key: snapshot.SensorId, value: out var sensor);

            if (!sensorExists)
            {
                return false;
            }

            var frame = GetOrCreateFrame(sensor.TrackingEntity, snapshot.Timestamp);
            frame.UpdateWithSnapshot(snapshot);
            SaveFrame(frame, sensor.TrackingEntity);

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
            var frameKey = lotid + "_" + timestamp.Date.Ticks;
            var exists = GetValue<ParkingLotFrame>(key: frameKey, value: out var frame);

            if (!exists)
            {
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
            var frameKey = lotid + "_" + frame.StartDateStamp.Date.Ticks;
            SetValue(key: frameKey, value: frame);
        }
    }
}
