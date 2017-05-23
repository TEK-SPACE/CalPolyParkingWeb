using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parkix.CurrentSensor.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Asset Links.
    /// </summary>
    public class PredixIeParkingAssetLinks
    {
        /// <summary>
        /// Gets or sets the href for the asset.
        /// </summary>
        /// <value>
        /// The self.
        /// </value>
        public PredixIeParkingAssetLinksHref Self { get; set; }

        /// <summary>
        /// Gets or sets the live events.
        /// </summary>
        /// <value>
        /// The live events.
        /// </value>
        [JsonProperty("live-events")]
        public PredixIeParkingAssetLinksHref LiveEvents { get; set; }
    }

}
