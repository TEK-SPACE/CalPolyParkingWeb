using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Parkix.Process.Entities.Timeseries
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Request.
    /// </summary>
    public class PredixTimeseriesQueryLatestDatapointRequestWithTimes : PredixTimeseriesQueryLatestDatapointRequest
    {

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        [JsonProperty("start")]
        public long Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        [JsonProperty("end")]
        public long End { get; set; }
    }
}
