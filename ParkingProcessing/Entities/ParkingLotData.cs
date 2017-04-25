using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities
{
    public class ParkingLotData
    {
        public string ParkingLotId { get; set; }

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
