using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Ingest Payload
    /// </summary>
    public class PredixTimeseriesIngestPayload
    {
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the data points.
        /// </summary>
        /// <value>
        /// The data points.
        /// </value>
        public List<Tuple<DateTime, Object, int>> DataPoints { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IDictionary<string, string> Attributes { get; set; } 
    }
}
