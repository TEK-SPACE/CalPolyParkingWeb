using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Process.Entities.Parking
{
    /// <summary>
    /// Parking Lot Detail.
    /// </summary>
    public class ParkingLotDetail : ParkingLotSummary
    {
        /// <summary>
        /// Gets or sets the spots.
        /// </summary>
        /// <value>
        /// The spots.
        /// </value>
        public List<ParkingSpot> Spots { get; set; }
    }
}
