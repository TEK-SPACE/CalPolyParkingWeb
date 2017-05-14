using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Result
    /// </summary>
    public class PredixIeParkingLatestDatapointResult
    {
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [JsonProperty("tags")]
        public List<PredixIeParkingLatestDatapointTag> Tags { get; set; }
    }
}
