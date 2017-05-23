using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Parkix.CurrentSensor.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Asset Web Socket Response.
    /// Contains an address to the websocket for the requested resource.
    /// </summary>
    public class PredixIeParkingAssetWebsocketResponse
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the registered events.
        /// </summary>
        /// <value>
        /// The registered events.
        /// </value>
        [JsonProperty("registered_events")]
        public string RegisteredEvents { get; set; }
    }
}
