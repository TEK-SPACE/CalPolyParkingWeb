using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Process.Entities.Parking
{
    /// <summary>
    /// Parking Lot Summary.
    /// </summary>
    public class ParkingLotSummary
    {
        /// <summary>
        /// Gets or sets the parking lot identifier.
        /// </summary>
        /// <value>
        /// The parking lot identifier.
        /// </value>
        public string ParkingLotId { get; set; }

        /// <summary>
        /// Gets or sets the parking lot status.
        /// </summary>
        /// <value>
        /// The parking lot status.
        /// </value>
        public string ParkingLotStatus { get; set; }

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

        /// <summary>
        /// Gets or sets the oldest timestamp.
        /// </summary>
        /// <value>
        /// The oldest timestamp.
        /// </value>
        public DateTime OldestTimestamp { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Gets or sets the newest timestamp.
        /// </summary>
        /// <value>
        /// The newest timestamp.
        /// </value>
        public DateTime NewestTimestamp { get; set; } = DateTime.MinValue;
    }
}
