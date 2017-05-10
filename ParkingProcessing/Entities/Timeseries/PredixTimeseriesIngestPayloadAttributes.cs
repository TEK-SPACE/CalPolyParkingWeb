using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Ingest Payload Attributes.
    /// </summary>
    public class PredixTimeseriesIngestPayloadAttributes
    {
        /// <summary>
        /// Gets or sets the parking lot identifier.
        /// </summary>
        /// <value>
        /// The parking lot.
        /// </value>
        public string ParkingLotId { get; set; }

        /// <summary>
        /// Gets or sets the spot identifier.
        /// </summary>
        /// <value>
        /// The spot identifier.
        /// </value>
        public string ParkingSpotId { get; set; }
    }
}
