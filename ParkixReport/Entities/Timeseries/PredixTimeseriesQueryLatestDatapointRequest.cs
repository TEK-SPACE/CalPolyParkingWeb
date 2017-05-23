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
    public class PredixTimeseriesQueryLatestDatapointRequest
    {
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [JsonProperty("tags")]
        public List<PredixTimeseriesQueryLatestDatapointTagNames> Tags { get; set; }
    }
}
