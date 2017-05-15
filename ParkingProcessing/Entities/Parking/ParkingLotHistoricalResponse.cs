using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Parking
{
    public class ParkingLotHistoricalResponse
    {
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

        /// <summary>
        /// Gets or sets the snapshots.
        /// </summary>
        /// <value>
        /// The snapshots.
        /// </value>
        public List<ParkingLotHistorialResponseSnapshot> Snapshots { get; set; }
    }
}
