using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using ParkingProcessing.Services;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Ingest Headers
    /// </summary>
    public class PredixTimeseriesIngestHeaders
    {
        /// <summary>
        /// Gets or sets the authorization.
        /// </summary>
        /// <value>
        /// The authorization.
        /// </value>
        public string Authorization { get; set; }

        /// <summary>
        /// Gets or sets the predix zone identifier.
        /// </summary>
        /// <value>
        /// The predix zone identifier.
        /// </value>
        [JsonProperty("Predix-Zone-Id")]
        public string PredixZoneId { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; set; }
    }
}
