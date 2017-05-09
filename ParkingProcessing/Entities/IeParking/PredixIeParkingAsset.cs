using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ParkingProcessing.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Asset
    /// </summary>
    public class PredixIeParkingAsset
    {
        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        [JsonProperty("_links")]
        public PredixIeParkingAssetLinks Links { get; set; }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        [JsonProperty("device-id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        [JsonProperty("event-type")]
        public string EventType { get; set; }
    }
}
