using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ParkingProcessing.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Request.
    /// </summary>
    public class PredixIeParkingLatestDatapointRequest
    {
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [JsonProperty("tags")]
        public List<PredixIeParkingLatestDatapointTagNames> Tags { get; set; }
    }
}
