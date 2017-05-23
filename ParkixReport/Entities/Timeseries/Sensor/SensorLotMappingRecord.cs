using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Process.Entities.Sensor
{
    /// <summary>
    /// Sensor to Lot Mapping Record.
    /// </summary>
    public class SensorLotMappingRecord
    {
        /// <summary>
        /// Gets or sets the parking lot identifier.
        /// </summary>
        /// <value>
        /// The parking lot identifier.
        /// </value>
        public string ParkingLotId { get; set; }

        /// <summary>
        /// Gets or sets the sensor identifier.
        /// </summary>
        /// <value>
        /// The sensor identifier.
        /// </value>
        public string SensorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this sensor is configurable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is configurable; otherwise, <c>false</c>.
        /// </value>
        public bool IsConfigurable { get; set; }
    }
}
