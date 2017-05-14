using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Configuration
{
    /// <summary>
    /// OpenCV Configuration
    /// </summary>
    public class ParkingSpotConfiguration
    {
        /// <summary>
        /// Gets or sets the spotid.
        /// </summary>
        /// <value>
        /// The spotid.
        /// </value>
        public int spotid { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public ParkingSpotConfigurationCoordinates coordinates { get; set; }
    }
}
