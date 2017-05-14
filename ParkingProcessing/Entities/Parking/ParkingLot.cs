using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Parking
{
    /// <summary>
    /// A representation of a parking lot consisting of parking spots.
    /// </summary>
    public class ParkingLot
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
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the spots.
        /// </summary>
        /// <value>
        /// The spots.
        /// </value>
        public List<ParkingSpot> ParkingSpots { get; set; }

    }
}
