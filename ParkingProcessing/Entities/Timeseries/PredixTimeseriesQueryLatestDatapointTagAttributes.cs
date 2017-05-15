using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Tag Attributes.
    /// </summary>
    public class PredixTimeseriesQueryLatestDatapointTagAttributes
    {
        /// <summary>
        /// Gets or sets the parking lot identifier.
        /// </summary>
        /// <value>
        /// The parking lot identifier.
        /// </value>
        public List<string> ParkingLotId { get; set; }

        /// <summary>
        /// Gets or sets the parking spot identifier.
        /// </summary>
        /// <value>
        /// The parking spot identifier.
        /// </value>
        public List<string> ParkingSpotId { get; set; }
    }
}
