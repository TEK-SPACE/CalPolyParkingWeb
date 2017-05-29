using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parkix.CurrentSensor.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Asset Search Result.
    /// </summary>
    public class PredixIeParkingAssetSearchResult
    {
        /// <summary>
        /// Gets or sets the embedded search result.
        /// </summary>
        /// <value>
        /// The embedded.
        /// </value>
        [JsonProperty("_embedded")]
        public PredixIeParkingSearchResultEmbedded Embedded { get; set; }
    }
}
