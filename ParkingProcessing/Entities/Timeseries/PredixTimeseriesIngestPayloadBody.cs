using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Ingest Payload Body.
    /// </summary>
    public class PredixTimeseriesIngestPayloadBody
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the datapoints.
        /// </summary>
        /// <value>
        /// The datapoints.
        /// </value>
        [JsonProperty("datapoints")]
        public List<List<object>> Datapoints { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        [JsonProperty("attributes")]
        public PredixTimeseriesIngestPayloadAttributes Attributes { get; set; }
    }
}
