using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities.Configuration;

namespace ParkingProcessing.Entities.Sensor
{
    /// <summary>
    /// Sensor Configuration. Indexed by sensor id.
    /// </summary>
    public class SensorConfiguration
    {
        /// <summary>
        /// Gets or sets the sensor identifier.
        /// </summary>
        /// <value>
        /// The sensor identifier.
        /// </value>
        public string SensorId { get; set; }

        /// <summary>
        /// Gets or sets the parking lot associated with the sensor.
        /// </summary>
        /// <value>
        /// The parking lot identifier.
        /// </value>
        public string ParkingLotId { get; set; }

        /// <summary>
        /// Tracks when the configuration was last downloaded.
        /// </summary>
        /// <value>
        /// The last accessed.
        /// </value>
        public DateTime LastAccessed { get; set; }

        /// <summary>
        /// Tracks when the configuration was last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public List<ParkingSpotConfiguration> Coordinates { get; set; }
    }
}
