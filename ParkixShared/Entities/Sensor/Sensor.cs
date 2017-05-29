using System;
using System.Collections.Generic;
using System.Text;

namespace Parkix.Shared.Entities.Sensor
{
    public class Sensor
    {
        /// <summary>
        /// Identifier for the sensor.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// The type of sensor.
        /// </summary>
        public string SensorType { get; set; }

        /// <summary>
        /// Gets or sets the tracking entity being tracked by this sensor.
        /// </summary>
        /// <value>
        /// The tracking entity.
        /// </value>
        public string TrackingEntity { get; set; }

        public ConfigurationStatus ConfigStatus { get; set; }
    }
}
