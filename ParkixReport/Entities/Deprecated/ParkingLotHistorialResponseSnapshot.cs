using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Process.Entities.Parking
{
    /// <summary>
    /// Parking Lot Historical Response Snapshot
    /// </summary>
    public class ParkingLotHistorialResponseSnapshot
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the parking spots total.
        /// </summary>
        /// <value>
        /// The parking spots total.
        /// </value>
        public int ParkingSpotsTotal { get; set; }

        /// <summary>
        /// Gets or sets the parking spots free.
        /// </summary>
        /// <value>
        /// The parking spots free.
        /// </value>
        public int ParkingSpotsFree { get; set; }

        /// <summary>
        /// Gets or sets the parking spots taken.
        /// </summary>
        /// <value>
        /// The parking spots taken.
        /// </value>
        public int ParkingSpotsTaken { get; set; }
    }
}
