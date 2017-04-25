using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParkingReporting.Entities
{
    public class PredixTimeseriesCredential
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the name of the zone HTTP header.
        /// </summary>
        /// <value>
        /// The name of the zone HTTP header.
        /// </value>
        [JsonProperty("zone-http-header-name")]
        public string ZoneHttpHeaderName { get; set; }

        /// <summary>
        /// Gets or sets the zone HTTP header value.
        /// </summary>
        /// <value>
        /// The zone HTTP header value.
        /// </value>
        [JsonProperty("zone-http-header-value")]
        public string ZoneHttpHeaderValue { get; set; }

        /// <summary>
        /// Gets or sets the zone token scopes.
        /// </summary>
        /// <value>
        /// The zone token scopes.
        /// </value>
        [JsonProperty("zone-token-scopes")]
        public List<string> ZoneTokenScopes { get; set; }
    }
}
